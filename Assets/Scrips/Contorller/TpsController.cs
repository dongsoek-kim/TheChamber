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
    /// <summary>
    /// 플레이어와 카메라 사이에 충돌체가있으면 카메라를 그 위치로 이동시켜주는 메서드
    /// </summary>
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
    }
}
