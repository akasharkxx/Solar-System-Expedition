using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomLaw : MonoBehaviour
{
    const float G = 667.4f;

    public static List<CustomLaw> customObjects;

    public Rigidbody rigidBody;

    private void FixedUpdate()
    {
        foreach(CustomLaw item in customObjects)
        {
            if(item != this)
            {
                Attract(item);
            }
        }
    }

    private void OnEnable()
    {
        if(customObjects == null)
        {
            customObjects = new List<CustomLaw>();
        }

        customObjects.Add(this);
    }

    private void OnDisable()
    {
        customObjects.Remove(this);
    }

    private void Attract(CustomLaw objToAtract)
    {
        Rigidbody rbToAtract = objToAtract.rigidBody;

        Vector3 direction = rigidBody.position - rbToAtract.position;
        float distance = direction.magnitude;

        if (distance == 0)
        {
            return;
        }

        float forceMagnitude = G * (rigidBody.mass * rbToAtract.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        rbToAtract.AddForce(force);
    }
}
