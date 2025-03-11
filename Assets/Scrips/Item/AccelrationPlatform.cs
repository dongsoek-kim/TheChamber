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
    /// 플레이어가 올라오면 플레이어에게 가속메서드 실행
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어 올라탐");
            playerController.AccelerationPlatform();
        }
    }

    /// <summary>
    /// 플레이어가 내려가면 가속종료 메서드 실행
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어 내려감");
            playerController.OnAccelerationPlatformExit();
        }
    }
}
