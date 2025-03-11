using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelrationPlatform : MonoBehaviour
{
    PlayerController playerController;

    private void Start()
    {
        playerController = CharacterManager.Instance.Player.controller;
    }

    /// <summary>
    /// �÷��̾ �ö���� �÷��̾�� ���Ӹ޼��� ����
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("�÷��̾� �ö�Ž");
            playerController.AccelerationPlatform();
        }
    }

    /// <summary>
    /// �÷��̾ �������� �������� �޼��� ����
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("�÷��̾� ������");
            playerController.OnAccelerationPlatformExit();
        }
    }
}
