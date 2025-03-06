using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class OilTank : MonoBehaviour
{
    public float amount = 30f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // �÷��̾�� �浹�ϸ�
        {
            Debug.Log("�÷��̾� �浹!");
            Use(amount);  // ���� ��� (ȸ��)
            Destroy(gameObject);  // ������ ��� �� ����
        }
    }
    public void Use(float amount)
    {
        CharacterManager.Instance.Player.oil.Add(amount);
        Debug.Log($"���� {amount} ȸ����! ���� ����: {CharacterManager.Instance.Player.oil.curValue}");
    }
}
