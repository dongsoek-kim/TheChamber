using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LaserContorller system;

    /// <summary>
    /// 레이저가 플레이어에게 닿으면 시스템 리셋
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            system.Reset();
        }
    }
}
