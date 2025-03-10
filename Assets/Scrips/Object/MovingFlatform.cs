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

    void Update()
    {
        float ping = Mathf.PingPong(Time.time * moveSpeed, 1f);
        transform.position = startPos + offset * ping;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = transform;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }
}
