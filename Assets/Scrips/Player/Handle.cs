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
    /// 손에 프리팹을 생성해주는 메서드
    /// </summary>
    /// <param name="data"></param>
    public void Equip(ItemData data)
    {
        if (!nowEuqipped)
        {
            Debug.Log("손에 프리팹생성");
            curHandleItem = Instantiate(data.handlePrefab, rightHand).GetComponent<HandleItem>();
            nowEuqipped = true;
        }
    }
    /// <summary>
    /// 손에든 프리팹을 파괴하는 메서드
    /// </summary>
   public void Arrange()
    {
        if (curHandleItem != null)
        {
            Debug.Log("손에 든 물건 파괴");
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
