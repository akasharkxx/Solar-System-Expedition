using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeRotationSpeed : MonoBehaviour
{
    private PlanetsNew[] planetsInScene = new PlanetsNew[8];
    private Slider sliderToChangeSpeed;
    private float previousValue;

    private void Start()
    {
        sliderToChangeSpeed = GetComponent<Slider>();
        planetsInScene = FindObjectsOfType<PlanetsNew>();

        previousValue = sliderToChangeSpeed.value;

        sliderToChangeSpeed.onValueChanged.AddListener(ChangeAlphaOfAll);
    }

    private void ChangeAlphaOfAll(float sliderValue)
    {
        for (int i = 0; i < planetsInScene.Length; i++)
        {
            planetsInScene[i].ChangeAlpha(sliderValue - previousValue);
        }

        Debug.Log(sliderValue - previousValue);
        previousValue = sliderValue;
    }
}
