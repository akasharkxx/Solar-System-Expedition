using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetLights : MonoBehaviour
{
    [SerializeField]
    private Transform[] planetLightTransforms;
    [SerializeField]
    private Transform[] planets;

    private void Update()
    {
        for(int i = 0; i < planets.Length; i++)
        {
            planetLightTransforms[i].LookAt(planets[i].transform);
        }
    }
}
