using UnityEngine;

public class SSVTarget : MonoBehaviour
{
    public float force = 300.0f;
    public float rayMax = 1000.0f;
    public LayerMask targetMask;
    public bool isInRange = false;
    public bool isForceNeeded = false;

    private Transform startPlanet, targetPlanet;

    private void Update()
    {
        if(isForceNeeded)
        {
            AddForceAlongTangent();
            isInRange = false;
        }
    }

    private void FixedUpdate()
    {
        if(!isForceNeeded)
        {
            CheckIfIsInRange();
        }
    }

    private void CheckIfIsInRange()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayMax, targetMask))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);
            //Debug.Log("In Range");
            isInRange = true;
        }
        else
        {
            //Debug.Log("In not in Range");
            isInRange = false;
        }
    }

    public void AddForceAlongTangent()
    {
        transform.position += transform.forward * force * Time.deltaTime;
    }

    public void SetPlanets(Transform _sPlanet, Transform _tPlanet)
    {
        startPlanet = _sPlanet;
        targetPlanet = _tPlanet;
    }
}
