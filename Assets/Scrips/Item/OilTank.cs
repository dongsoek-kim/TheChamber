using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class OilTank : MonoBehaviour
{
    public float amount = 30f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // 플레이어와 충돌하면
        {
            Debug.Log("플레이어 충돌!");
            Use(amount);  // 오일 사용 (회복)
            Destroy(gameObject);  // 아이템 사용 후 삭제
        }
    }
    public void Use(float amount)
    {
        CharacterManager.Instance.Player.oil.Add(amount);
        Debug.Log($"오일 {amount} 회복됨! 현재 오일: {CharacterManager.Instance.Player.oil.curValue}");
    }
}
