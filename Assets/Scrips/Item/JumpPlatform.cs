using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : HandleItem
{
    /// <summary>
    /// ���� �÷����� �÷��̾ �ö���� �޼������
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("«��");
            CharacterManager.Instance.Player.controller.JumpPlatform(data.jumpPower);

        }
    }
}
