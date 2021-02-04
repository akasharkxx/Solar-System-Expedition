using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsNew : MonoBehaviour
{
    public Transform SunTransform;
    public float centerX, centerY;
    public float semiMajorA, semiMinorB;
    public float alphaIncrease, alphaMultiplier;
    public bool isClockWise;
    public bool isMercury;

    private float xCordinate, yCordinate;
    private float alpha, alphaForMecury;

    private void Update()
    {
        if(isClockWise)
        {
            alpha -= alphaIncrease;
        }
        else
        {
            alpha += alphaIncrease;
        }
        
        if(isMercury)
        {
            CalculateNewCenters();
        }

        xCordinate = centerX + (semiMajorA * Mathf.Cos(alpha * alphaMultiplier));
        yCordinate = centerY + (semiMinorB * Mathf.Sin(alpha * alphaMultiplier));
        this.gameObject.transform.position = new Vector3(xCordinate, 0f, yCordinate);

        transform.Rotate(new Vector3(0f, 1f, 0f), Space.Self);
    }

    private void CalculateNewCenters()
    {
        alphaForMecury -= 0.5f;
        float newX = 13.5f * Mathf.Cos(alphaForMecury * 0.01f);
        float newY = 11.5f * Mathf.Sin(alphaForMecury * 0.01f);
        //Debug.Log($"New X = {newX} and New Y = {newY}");
        centerX = newX;
        centerY = -newY;
    }

    public void ChangeAlpha(float change)
    {
        alphaIncrease += change;
        Debug.Log($"{gameObject.name} alpha value = {alphaMultiplier}");
        //Debug.Log(multiplier);
    }
}
