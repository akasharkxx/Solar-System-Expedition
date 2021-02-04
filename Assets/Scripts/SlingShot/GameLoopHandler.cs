using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameLoopHandler : MonoBehaviour
{
    public int numberOfChances = 5;
    public int requiredToWin = 3;
    public Transform startPlanet, targetPlanet;
    public GameObject playerShip;
    public TextMeshProUGUI hitMiss;
    public GameObject panel;

    private bool isComingAgain = false;
    private AssitTarget aT;
    private AssitRotation aR;
    private SlingShotManager sTM;

    public ParticleSystem explosion;

    private int currentAttempt = 1;
    private float elapsedTime = 0.0f; 

    private void Awake()
    {
        aT = playerShip.GetComponent<AssitTarget>();
        aR = playerShip.GetComponent<AssitRotation>();
        sTM = playerShip.GetComponent<SlingShotManager>();

        aT.SetPlanet(targetPlanet);
        aR.SetPlanets(startPlanet, targetPlanet);
    }

    private void Start()
    {
        hitMiss.text = "";
        aR.ChangeAlphaIncreaseFromOutSide(60.0f);
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime > 4.0f && panel.activeSelf)
        {
            Debug.Log("After 4 sec");
            aR.ChangeAlphaIncreaseFromOutSide(5.0f);
            panel.SetActive(false);
        }

        if ((Vector3.Distance(playerShip.transform.position, startPlanet.position) > 400f) && (Vector3.Distance(playerShip.transform.position, targetPlanet.position) > 100f))
        {
            //Debug.Log("Is far from both");
            StartCoroutine(RestIfMiss());
        }
        if (aT.isNearToTarget)
        {
            StartCoroutine(ResetIfHit());
        }

        AfterTry();
    }

    private void AfterTry()
    {
        if ((Vector3.Distance(playerShip.transform.position, startPlanet.position) <= 100.0f) && isComingAgain)
        {
            aT.enabled = true;
            isComingAgain = false;
        }
    }

    private IEnumerator ResetIfHit()
    {

        hitMiss.text = "Hit";
        Debug.Log("Hit");
        aR.enabled = true;

        explosion.Play();

        //sTM.camAnim.Play("Cam4");

        yield return new WaitForSeconds(2.0f);

        //explosion.Stop();
        aT.ResetTemps();
        aT.enabled = false;
        sTM.EnableControls();
        aT.isNearToTarget = false;
        isComingAgain = true;
        aR.ChangePlanet(startPlanet);
    }

    private IEnumerator RestIfMiss()
    {
        hitMiss.text = "Miss";
        Debug.Log("Miss");

        yield return new WaitForSeconds(2.0f);

        aR.enabled = true;
        aR.ChangePlanet(startPlanet);
        aT.enabled = false;
        isComingAgain = true;
        aT.ResetTemps();
        sTM.EnableControls();
    }
}
