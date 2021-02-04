using UnityEngine;

public class CameraStateChange : MonoBehaviour
{
    private Animator animator;
    private bool cam1 = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SwitchState();
        }

    }

    private void SwitchState()
    {
        if(cam1)
        {
            animator.Play("Cam1");
        }
        else
        {
            animator.Play("Cam2");
        }
        cam1 = !cam1;
    }

}
