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

    public void Equip(ItemData data)
    {
        if (!nowEuqipped)
        {
            Debug.Log("손에 프리팹생성");
            curHandleItem = Instantiate(data.handlePrefab, rightHand).GetComponent<HandleItem>();
            nowEuqipped = true;
        }
    }

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
