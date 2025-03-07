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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어 올라탐");
            playerController.AccelerationPlatform();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어 내려감");
            playerController.OnAccelerationPlatformExit();
        }
    }
}
