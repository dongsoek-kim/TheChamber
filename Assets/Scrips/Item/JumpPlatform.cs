using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : HandleItem
{
    public ItemData data;

    public float jumpPower;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("б╚га");
            CharacterManager.Instance.Player.controller.JumpPlatform(jumpPower);

        }
    }
}
