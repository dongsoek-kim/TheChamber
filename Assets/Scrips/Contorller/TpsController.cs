using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpsController : MonoBehaviour
{
    [Header("Transform")]
    [SerializeField] private Transform initTransform;   
    [SerializeField] private Transform PlayerTransform;   

    float distance; 

    public float checkRate = 0.05f;
    private float lastCheckTime;

    private void Awake()
    {
        distance = Vector3.Distance(PlayerTransform.position, initTransform.position);
    }

    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            RaycastFromPlayer();
        }
    }

    private void RaycastFromPlayer()
    {
        Vector3 direction = (initTransform.position - PlayerTransform.position).normalized;
        Ray ray = new Ray(PlayerTransform.position, direction);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance))
        {
            float offset = 0.2f; 
            float newDist = Mathf.Max(hit.distance - offset, 0f);
            this.transform.position = PlayerTransform.position + direction * newDist;
        }
        else
        {
            this.transform.position = initTransform.position;
        }


        Debug.DrawRay(PlayerTransform.position, direction * distance, Color.red, checkRate);
    }
}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TpsController : MonoBehaviour
//{
//    [SerializeField] private Transform initTransform;
//    [SerializeField] private Transform PlayerTrnasform;
//    float distance;

//    public float checkRate = 0.05f;
//    private float lastCheckTime;

//    void Update()
//    {
//        if (Time.time - lastCheckTime > checkRate)
//        {
//            lastCheckTime = Time.time;
//            RaycastController.ProcessRaycast(distance, HandleChangeTransform);
//        }
//    }
//    private void Awake()
//    {
//        distance = Vector3.Distance(initTransform.position, PlayerTrnasform.position);
//    }
//    private void HandleChangeTransform(RaycastHit hit)
//    {
//        Vector3 cameradir = (initTransform.position - PlayerTrnasform.position).normalized;
//        float hitDistance = Vector3.Distance(PlayerTrnasform.position, hit.point);
//        if (hitDistance < distance)
//        {
//            float offset = 0.2f; 
//            float newDist = Mathf.Max(hitDistance - offset, 0f);
//            this.transform.position = PlayerTrnasform.position + cameradir * newDist;
//        }
//        else 
//        {
//            this.transform.position = initTransform.position;
//        }
//    }
//}
