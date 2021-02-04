using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDirections : MonoBehaviour
{
    public Transform startPlanet, targetPlanet;
    public float length = 300.0f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, (transform.forward * length) + transform.position);
    }
}
