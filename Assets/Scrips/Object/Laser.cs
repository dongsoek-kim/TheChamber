using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LaserContorller system;

    /// <summary>
    /// �������� �÷��̾�� ������ �ý��� ����
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
