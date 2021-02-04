using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SSVGameLoopHandle : MonoBehaviour
{
    //Game Objects and Transforms
    public Transform startPlanet;
    public Transform targetPlanet;
    public GameObject playerShip;

    //Animation and Effects
    public ParticleSystem explosion;
    public Animator camAnim;

    //UIs
    public Slider speedSlider;
    public Button shootButton;
    public GameObject targetArrow;
    public GameObject messagePanel;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI remainTries;
    public TextMeshProUGUI winsText;

    //Integers
    public int numberOfChances = 5;
    public int requiredToWin = 3;

    //Floats
    public float alphaIncMultiplier = 5.0f;
    public float minDistanceToHit = 50.0f;
    public float waitTimeForNextTry = 4.0f;

    //Privates

    private float currentTry, triesWon;
    private bool isHit = false;

    //Other Scripts
    private SSVRotation SSVR;
    private SSVTarget SSVT;

    private void Awake()
    {
        SSVR = playerShip.GetComponent<SSVRotation>();
        SSVT = playerShip.GetComponent<SSVTarget>();

        //Rotation
        SSVR.SetPlanets(startPlanet, targetPlanet);

        //Target
        SSVT.SetPlanets(startPlanet, targetPlanet);

        currentTry = 0;
        //Debug.Log(Vector3.Distance(targetPlanet.position, startPlanet.position));
    }

    private void Start()
    {
        speedSlider.onValueChanged.AddListener(ChangeSpeedAndRadius);
        shootButton.onClick.AddListener(ForceAlongTangent);
        ShowMessage(true, "Start");
        StartCoroutine(InBetweenAnimation());
    }

    private void Update()
    {
        UpdateUI();
        PlayAnimationCamera();
        if (SSVT.isForceNeeded)
        {
            float distanceFromTarget = Vector3.Distance(playerShip.transform.position, targetPlanet.position);
            float distanceFromStart = Vector3.Distance(playerShip.transform.position, startPlanet.position);

            //camAnim.Play("Going");

            //Debug.Log(distanceFromTarget);
            if (distanceFromTarget <= minDistanceToHit)
            {
                Debug.Log("Hit");
                SSVT.isForceNeeded = false;
                SSVR.enabled = true;
                SSVR.SetCurrentPlanet(targetPlanet);
                explosion.Play();
                isHit = true;
                //camAnim.Play("Explosion");
                //ShowMessage(true, "Attempt Successful");
                
                CheckRemainingTry(true);

                StartCoroutine(InBetweenAnimation());
                //isHit = false;

                SSVR.SetCurrentPlanet(startPlanet);
                //ResetForNextTry
            }
            if (distanceFromTarget >= minDistanceToHit && distanceFromStart >= 1500.0f)
            {
                Debug.Log("Miss");
                SSVT.isForceNeeded = false;
                ShowMessage(true, "TryAgain");
                isHit = false;
                CheckRemainingTry(false);

                StartCoroutine(InBetweenAnimation());
                //Show Try again message

                SSVR.enabled = true;
            }
        }
    }

    private IEnumerator InBetweenAnimation()
    {
        yield return new WaitForSeconds(waitTimeForNextTry);
        isHit = false;
        ShowMessage(false, null);
    }

    private void CheckRemainingTry(bool WonOrLost)
    {
        if(WonOrLost)
        {
            triesWon++;
        }
        currentTry++;
        winsText.text = "Wins: " + triesWon.ToString();
        remainTries.text = "Remaining Tries: " + (numberOfChances - currentTry).ToString();
        //Debug.Log($"Remaining Tries: {numberOfChances - currentTry}");
        //Debug.Log($"Chances Won: {triesWon}");
        //Debug.Log($"Current Try: {currentTry}");
        if(currentTry == 5)
        {
            if (triesWon >= 3)
            {
                ShowMessage(true, "Won");
            }
            else
            {
                ShowMessage(true, "Lost");
            }
        } 
    }

    private void ShowMessage(bool activate, string message)
    {
        messagePanel.SetActive(activate);
        messageText.text = message;
    }

    private void UpdateUI()
    {
        if (SSVT.isInRange)
        {
            targetArrow.SetActive(true);
            shootButton.interactable = true;
            RotateTargetArrow();
            //camAnim.Play("Target");
        }
        else
        {
            shootButton.interactable = false;
            targetArrow.SetActive(false);
            //camAnim.Play("Rotation");
            
        }
    }

    private void PlayAnimationCamera()
    {
        if(SSVT.isForceNeeded && !isHit)
        {
            camAnim.Play("Going");
            Debug.Log("Going");
            //playNext = true;
        }
        else if(!SSVT.isForceNeeded && SSVT.isInRange && !isHit)
        {
            camAnim.Play("Target");
            Debug.Log("Target");
        }
        else if(!isHit && !SSVT.isForceNeeded && !SSVT.isInRange)
        {
            camAnim.Play("Rotation");
            Debug.Log("Rotation");
        }
        else if(isHit)
        {
            camAnim.Play("Explosion");
            Debug.Log("Explosion");
        }
    }

    private void RotateTargetArrow()
    {
        Vector3 newRotT = Vector3.zero;
        Vector3 rotP = playerShip.transform.rotation.eulerAngles;

        newRotT.z = -(rotP.y + 20f);

        targetArrow.transform.rotation = Quaternion.Euler(newRotT);
    }

    private void ChangeSpeedAndRadius(float arg0)
    {
        SSVR.ChangeAlphaInc(speedSlider.value * alphaIncMultiplier);
    }

    private void ForceAlongTangent()
    {
        SSVT.isForceNeeded = true;
        SSVR.enabled = false;
    }
}
