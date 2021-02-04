using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltObject : MonoBehaviour
{
    [SerializeField]
    private float orbitSpeed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private bool rotationClockwise;
    [SerializeField]
    private GameObject parent;
    [SerializeField]
    private Vector3 rotationDirection;

    public void SetupBeltObject(float _speed, float _rotationSpeed, GameObject _parent, bool _rotateClockwise)
    {
        orbitSpeed = _speed;
        rotationSpeed = _rotationSpeed;
        rotationClockwise = _rotateClockwise;
        parent = _parent;
        rotationDirection = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
    }

    private void Update()
    {
        Vector3 position = parent.transform.position;
        if (rotationClockwise)
        {
            transform.RotateAround(position, parent.transform.up, orbitSpeed * Time.deltaTime);
        }
        else
        {
            transform.RotateAround(position, -parent.transform.up, orbitSpeed * Time.deltaTime);
        }

        transform.Rotate(rotationDirection, rotationSpeed * Time.deltaTime);
    }

    public string GetParent()
    {
        return parent.name;
    }
}
