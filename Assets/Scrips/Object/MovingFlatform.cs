using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFlatform : MonoBehaviour
{
    [Header("MoveingPosition and Speed")]
    public float moveX = 0f;
    public float moveY = 0f;
    public float moveZ = 0f;
    public float moveSpeed = 1f;
    private Vector3 startPos;
    private Vector3 offset;
    void Start()
    {
        startPos = transform.position;
        offset = new Vector3(moveX, moveY, moveZ);

    }
    /// <summary>
    /// 매 프레임마다 인스펙터에서 지정해준 위치로 이동
    /// 핑퐁을 통해 돌아옴
    /// </summary>
    void Update()
    {
        float ping = Mathf.PingPong(Time.time * moveSpeed, 1f);
        transform.position = startPos + offset * ping;
    }

    /// <summary>
    /// 플레이어가 올라탈경우 자식으로 만들어줘서 같이 움직임
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = transform;
        }
    }

    /// <summary>
    /// 플레이가 내려가면 자식 해제
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }
}
