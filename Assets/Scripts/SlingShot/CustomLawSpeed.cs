using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomLawSpeed : MonoBehaviour
{
    public float radiusR = 384f;
    public float yearT = 12f;
    public CustomLaw earth;

    private Rigidbody rb;
    private Rigidbody earthRb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        earthRb = earth.gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float mag = (2 * Mathf.PI * radiusR) / yearT;
        Vector3 direction = earthRb.position - rb.position;
        rb.velocity =  direction.normalized * mag;
    }
}
