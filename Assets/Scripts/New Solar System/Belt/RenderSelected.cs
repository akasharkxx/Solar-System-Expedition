using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RenderSelected : MonoBehaviour
{
    public int objects = 500;
    public Transform sun;

    private Camera cameraInScene;
    
    private MeshRenderer[] meshRenderer;
    private Transform[] pointsInSpace;
    private BeltObject[] cubes;


    private Plane[] planes = new Plane[6];

    private float left, right, down, up, distanceFromSun;

    private void Start()
    {
        meshRenderer = new MeshRenderer[objects];
        pointsInSpace = new Transform[objects];
        
        // Get Camera
        cameraInScene = GetComponent<Camera>();

        //Get all Belt Objects
        cubes = FindObjectsOfType<BeltObject>(); 

        for(int i = 0; i < cubes.Length; i++)
        {
            pointsInSpace[i] = cubes[i].transform;
        }
        //cubes.Clear();
        //Clearing list after use;

        // Get all Mesh Renderers;
        for(int i = 0; i < objects; i++)
        {
            meshRenderer[i] = pointsInSpace[i].gameObject.GetComponent<MeshRenderer>();
        }

        distanceFromSun = Vector3.Distance(this.transform.position, sun.position);

        //Repeating after Sometimes
        InvokeRepeating("CheckIfObjectIsIn", 0.5f, 0.5f);   
    }

    private void Update()
    {
        distanceFromSun = Vector3.Distance(this.transform.position, sun.position);
    }

    private void CheckIfObjectIsIn()
    {
        
        for (int i = 0; i < objects; i++)
        {
            if(distanceFromSun <= 600.0f)
            {
                if (cubes[i].GetParent() == "Kupier Belt")
                {
                    cubes[i].gameObject.SetActive(false);
                }
                else
                {
                    cubes[i].gameObject.SetActive(true);
                    GeometryUtility.CalculateFrustumPlanes(cameraInScene, planes);
                    left = planes[0].GetDistanceToPoint(pointsInSpace[i].position);
                    right = planes[1].GetDistanceToPoint(pointsInSpace[i].position);
                    down = planes[2].GetDistanceToPoint(pointsInSpace[i].position);
                    up = planes[3].GetDistanceToPoint(pointsInSpace[i].position);

                    if (left >= 0 && right >= 0 && up >= 0 && down >= 0)
                    {
                        meshRenderer[i].enabled = true;
                    }
                    else
                    {
                        meshRenderer[i].enabled = false;
                    }
                }
            }
            else
            {
                if (cubes[i].GetParent() == "Kupier Belt")
                {
                    cubes[i].gameObject.SetActive(true);
                    GeometryUtility.CalculateFrustumPlanes(cameraInScene, planes);
                    left = planes[0].GetDistanceToPoint(pointsInSpace[i].position);
                    right = planes[1].GetDistanceToPoint(pointsInSpace[i].position);
                    down = planes[2].GetDistanceToPoint(pointsInSpace[i].position);
                    up = planes[3].GetDistanceToPoint(pointsInSpace[i].position);

                    if (left >= 0 && right >= 0 && up >= 0 && down >= 0)
                    {
                        meshRenderer[i].enabled = true;
                    }
                    else
                    {
                        meshRenderer[i].enabled = false;
                    }
                }
                else
                {
                    cubes[i].gameObject.SetActive(false);
                }
            }
            
        }
    }
}
