using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Handle : MonoBehaviour
{
    public HandleItem curHandleItem;
    public Transform rightHand;

    private PlayerController controller;
    bool nowEuqipped;

    public void Start()
    {
        controller=GetComponent<PlayerController>();
    }
    /// <summary>
    /// �տ� �������� �������ִ� �޼���
    /// </summary>
    /// <param name="data"></param>
    public void Equip(ItemData data)
    {
        if (!nowEuqipped)
        {
            Debug.Log("�տ� �����ջ���");
            curHandleItem = Instantiate(data.handlePrefab, rightHand).GetComponent<HandleItem>();
            nowEuqipped = true;
        }
    }
    /// <summary>
    /// �տ��� �������� �ı��ϴ� �޼���
    /// </summary>
   public void Arrange()
    {
        if (curHandleItem != null)
        {
            Debug.Log("�տ� �� ���� �ı�");
            Destroy(curHandleItem.gameObject);
            nowEuqipped=false;
            curHandleItem = null;
        }
    }

    public bool NowEuqipped()
    {
        return nowEuqipped;
    }
}
