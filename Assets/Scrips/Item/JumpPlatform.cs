using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : HandleItem
{
    /// <summary>
    /// 점프 플랫폼에 플레이어가 올라오면 메서드실행
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("짬프");
            CharacterManager.Instance.Player.controller.JumpPlatform(data.jumpPower);

        }
    }
}
