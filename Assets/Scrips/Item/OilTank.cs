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
    /// �÷��̾ Ʈ���ſ� ������ �÷��̾� ü�� ȸ�� �� ������Ʈ ��Ȱ��ȭ
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            Debug.Log("�÷��̾� �浹!");
            Use(amount);  
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// �÷��̾� ä��ȸ�� �޼���
    /// </summary>
    /// <param name="amount"></param>
    public void Use(float amount)
    {
        CharacterManager.Instance.Player.oil.Add(amount);
        Debug.Log($"���� {amount} ȸ����! ���� ����: {CharacterManager.Instance.Player.oil.curValue}");
    }

    /// <summary>
    /// ��Ȱ��ȭ �޼���
    /// </summary>
    public void Reset()
    {
        gameObject.SetActive(true);
    }
}
