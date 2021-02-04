using UnityEngine;

public class AssitTarget : MonoBehaviour
{
    public Transform targetPlanet;
    public float minDistance = float.MaxValue;
    public float addFloat = 10.0f;

    public bool isNearToTarget = false;

    private float distance;
    private Vector3 direction, add;
    public bool isForceApplied = false;

    private void Start()
    {
        add = new Vector3(0f, 0f, 0f);
    }

    private void Update()
    {
        if (!isForceApplied)
        {
            FindLaunchRange();
        }
        CheckifReachedTarget();

        //Debug.Log(isForceApplied);
    }

    private void FindLaunchRange()
    {
        direction = targetPlanet.position - transform.position;
        distance = direction.magnitude;
        
        UpdateMinDistance();
    }

    private void UpdateMinDistance()
    {
        if(distance <= minDistance)
        {
            minDistance = distance;
        }
        //Debug.Log(minDistance);
    }

    public float GetMinimumDistance()
    {
        return minDistance;
    }

    public float GetCurrentDistance()
    {
        return distance;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, targetPlanet.position + add);
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 300.0f);
    }

    public void AddForce(float force, float addF)
    {
        add.x = addF;

        transform.position += (direction + add).normalized * force * Time.deltaTime;
        isForceApplied = true;
        //Debug.Log("Applying Force");
        //Debug.Log(direction.normalized);
    }

    private void CheckifReachedTarget()
    {
        if(Vector3.Distance(transform.position, targetPlanet.position) <= 30.0f)
        {
            //Debug.Log($"Near {targetPlanet.gameObject.name}");
            isNearToTarget = true;
            isForceApplied = false;
        }
        else
        {
            isNearToTarget = false;
        }
    }

    public void SetPlanet(Transform _targetPlanet)
    {
        targetPlanet = _targetPlanet;
    }

    public void ResetTemps()
    {
        add.x = 0.0f;
        minDistance = float.MaxValue;
        isForceApplied = false;
        //Debug.Log("reset Temps aT");
    }
}
