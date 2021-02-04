using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssitRotation : MonoBehaviour
{
    public Transform startPlanet, targetPlanet;
    public float centerX, centerY;
    public float radius = 20.0f;
    public float speed = 0.5f;
    public float alphaIncrease, alphaMultiplier;
    public bool isClockwise;
    public Slider speedSlider;

    private float xCordinate, yCordinate;
    private float alpha, defaultRadius;
    private Vector3 newPosition = Vector3.zero;

    private void Start()
    {
        defaultRadius = radius;

        centerX = startPlanet.position.x;
        centerY = startPlanet.position.z;

        speedSlider.wholeNumbers = true;
        speedSlider.onValueChanged.AddListener(ChangeAlphaIncrease);
    }

    private void Update()
    {
        Revolution();
    }

    private void Revolution()
    {
        if (isClockwise)
        {
            alpha -= alphaIncrease;
        }
        else
        {
            alpha += alphaIncrease;
        }

        xCordinate = centerX + (radius * Mathf.Cos(alpha * alphaMultiplier));
        yCordinate = centerY + (radius * Mathf.Sin(alpha * alphaMultiplier));
        newPosition.x = xCordinate;
        newPosition.z = yCordinate;
        transform.LookAt(newPosition);
        this.transform.position = newPosition;
    }

    private void ChangeAlphaIncrease(float sliderValue)
    {
        int value = Mathf.FloorToInt(sliderValue);

        switch(value)
        {
            case 1:
                alphaIncrease = 5;
                ChangeRadius(0);
                break;
            case 2:
                alphaIncrease = 10;
                ChangeRadius(10);
                break;
            case 3:
                alphaIncrease = 15;
                ChangeRadius(20);
                break;
            default:
                alphaIncrease = 5;
                radius = defaultRadius;
                break;
        }
    }

    public float ChangeAlphaIncreaseFromOutSide(float value)
    {
        float returnValue = alphaIncrease;
        alphaIncrease = value;
        return returnValue;
    }

    private void ChangeRadius(int value)
    {
        radius = defaultRadius + value;
    }

    public void ChangePlanet()
    {
        centerX = targetPlanet.position.x;
        centerY = targetPlanet.position.z;
        radius = 60.0f;
        ChangeAlphaIncrease(speedSlider.value * 5.0f);
    }

    public void ChangePlanet(Transform _planet)
    {
        centerX = _planet.position.x;
        centerY = _planet.position.z;
        radius = defaultRadius;
        ChangeAlphaIncrease(speedSlider.value * 5.0f);
    }

    public void SetPlanets(Transform _startPlanet, Transform _targetPlanet)
    {
        startPlanet = _startPlanet;
        targetPlanet = _targetPlanet;
    }
}
