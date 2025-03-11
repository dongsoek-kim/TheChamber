using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class OilTank : MonoBehaviour
{
    public float amount = 30f;

    private void Start()
    {
        CharacterManager.Instance.Player.controller.reStart += Reset;
    }
    /// <summary>
    /// 플레이어가 트리거에 들어오면 플레이어 체력 회복 및 오브젝트 비활성화
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            Debug.Log("플레이어 충돌!");
            Use(amount);  
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 플레이어 채력회복 메서드
    /// </summary>
    /// <param name="amount"></param>
    public void Use(float amount)
    {
        CharacterManager.Instance.Player.oil.Add(amount);
        Debug.Log($"오일 {amount} 회복됨! 현재 오일: {CharacterManager.Instance.Player.oil.curValue}");
    }

    /// <summary>
    /// 재활성화 메서드
    /// </summary>
    public void Reset()
    {
        gameObject.SetActive(true);
    }
}
