using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlingShotManager : MonoBehaviour
{
    public AssitRotation assitRot;
    public AssitTarget assistTarg;

    public float range = 20f, distanceWin = 5.0f, force = 200.0f;

    public GameObject targetArrow;
    public Button targetButton;
    public Slider speedSlider;

    public Animator camAnim;

    private bool isInRange = false, isForceNeeded = false;
    private float previousAlpha = 0.0f;
    private float previousDiff = 0.0f, previousDistance = 0.0f, change = 0.0f;
    private int count = 0;

    private void Start()
    {
        targetButton.onClick.AddListener(SlowRotation);
    }

    private void Update()
    {
        
        if (assistTarg.isNearToTarget)
        {
            assitRot.enabled = true;
            assitRot.ChangePlanet();
            camAnim.Play("Cam4");
        }
        else
        {
            //Debug.Log("Is Away");
            MoveArrowAndChangeDirection();
        }

        //PlayAnimations();
    }

    private void MoveArrowAndChangeDirection()
    {
        if (assistTarg.GetCurrentDistance() <= (assistTarg.GetMinimumDistance() + range))
        {
            targetArrow.SetActive(true);
            isInRange = true;

            float diff = assistTarg.GetCurrentDistance() - assistTarg.GetMinimumDistance();
            previousDiff = assistTarg.GetCurrentDistance() - previousDistance;
            previousDistance = assistTarg.GetCurrentDistance();

            change = assistTarg.GetCurrentDistance() - assistTarg.GetMinimumDistance();

            Vector3 arrowRotate = targetArrow.transform.rotation.eulerAngles;
            
            //Debug.Log(arrowRotate); 
            
            if (previousDiff < 0)
            {
                arrowRotate.z = change;
            }
            else
            {
                arrowRotate.z = -change;
            }
            targetArrow.transform.rotation = Quaternion.Euler(arrowRotate);

            //Debug.Log(change);

            if (isForceNeeded)
            {
                camAnim.Play("Cam3");
                if (previousDiff < 0)
                {
                    assistTarg.AddForce(force, change * 4);
                }
                else
                {
                    assistTarg.AddForce(force, -(change * 4));
                }
            }
            else
            {
                camAnim.Play("Cam2");
            }
        }
        else
        {
            targetArrow.SetActive(false);
            camAnim.Play("Cam1");
            isInRange = false;
            previousDistance = assistTarg.GetCurrentDistance();
            if (count > 0)
            {
                assitRot.ChangeAlphaIncreaseFromOutSide(previousAlpha);
                count = 0;
                isForceNeeded = false;
                targetButton.interactable = true;
                speedSlider.interactable = true;
            }
        }
    }

    private void SlowRotation()
    {
        if (isInRange)
        {
            previousAlpha = assitRot.ChangeAlphaIncreaseFromOutSide(0.1f);
            count++;
            targetButton.interactable = false;
            speedSlider.interactable = false;
            assitRot.enabled = false;
            isForceNeeded = true;
            //assistTarg.enabled = false;
        }
    }

    public float ClampAngle(float angle, float from, float to)
    {
        // accepts e.g. -80, 80
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }

    private void PlayAnimations()
    {
        if(isInRange)
        {
            camAnim.Play("Cam2");
        }
        else if(isForceNeeded)
        {
            camAnim.Play("Cam3");
        }
        else if(assistTarg.isNearToTarget)
        {
            camAnim.Play("Cam4");
        }
        else
        {
            camAnim.Play("Cam1");
        }

    }

    public void EnableControls()
    {
        speedSlider.interactable = true;
        targetButton.interactable = true;
        targetArrow.transform.rotation = Quaternion.identity;
        change = 0.0f;
        previousAlpha = speedSlider.value * 5.0f;
        Debug.Log(speedSlider.value);
        previousDiff = 0.0f;
        previousDistance = 0.0f;
        isForceNeeded = false;
        isInRange = false;
    }
}
