using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    void PickUp();
}

public abstract class HandleItem : MonoBehaviour, IItem
{
    public ItemData data;


    public void PickUp()
    {
        Debug.Log("�÷��̾�� ��������");
        CharacterManager.Instance.Player.itemData = data;
        Destroy(gameObject);
    }
}

public abstract class KeyCard : MonoBehaviour, IItem
{
    public ItemData data;

    public int index;
    public void PickUp()
    {
    }
    public abstract void Use(int index);
}
