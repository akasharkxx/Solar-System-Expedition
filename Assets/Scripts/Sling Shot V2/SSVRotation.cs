using UnityEngine;

public class SSVRotation : MonoBehaviour
{
    public bool isClockwise = true;
    public float radiusA = 80.0f;
    public float radiusB = 40.0f;
    public float alphaInc = 5.0f;
    public float alphaMul = 0.001f;

    private Transform startPlanet, targetPlanet, currentPlanet;

    private Vector3 newPos = Vector3.zero;

    private float radius;
    private float xCenter, yCenter;
    private float xCord, yCord;
    private float alpha, defaultAlphaInc = 5.0f;

    private void Start()
    {
        SetCurrentPlanet(startPlanet);
        SetRadius();
    }

    private void Update()
    {
        Revolution();
    }

    private void Revolution()
    {
        if(isClockwise)
        {
            alpha -= alphaInc;
        }
        else
        {
            alpha += alphaInc;
        }

        xCord = xCenter + (radius * Mathf.Cos(alpha * alphaMul));
        yCord = yCenter + (radius * Mathf.Sin(alpha * alphaMul));

        newPos.x = xCord;
        newPos.z = yCord;

        transform.LookAt(newPos);

        this.transform.position = newPos;
    }

    public void SetPlanets(Transform _sPlanet, Transform _tPlanet)
    {
        startPlanet = _sPlanet;
        targetPlanet = _tPlanet;
    }
    public void SetCurrentPlanet(Transform _curPlanet)
    {
        currentPlanet = _curPlanet;
        xCenter = currentPlanet.position.x;
        yCenter = currentPlanet.position.z;
    }
    public void SetRadius()
    {
        if(currentPlanet == startPlanet)
        {
            radius = radiusA;
        }
        if(currentPlanet == targetPlanet)
        {
            radius = radiusB;
        }
    }
    public void ChangeAlphaInc(float value)
    {
        alphaInc = value;
    }
}
