using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    [SerializeField]
    private float speed = 10.0f;

    [SerializeField]
    private float rotateAngle = 3.0f;

    private FloatingJoystick floatingJoystick;
    private float horizontalInput, verticalInput;

    private void Start()
    {
        floatingJoystick = FindObjectOfType<FloatingJoystick>();
    }

    private void Update()
    {
        //Debug.Log(floatingJoystick.Horizontal);
        //Debug.Log(floatingJoystick.Vertical);

#if UNITY_ANDROID
        horizontalInput = floatingJoystick.Horizontal;
        verticalInput = floatingJoystick.Vertical;
#endif
#if UNITY_EDITOR
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
#endif


        transform.position += transform.forward * verticalInput * speed * Time.deltaTime;

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + horizontalInput * rotateAngle * Time.deltaTime, transform.rotation.eulerAngles.z);
    }
}
