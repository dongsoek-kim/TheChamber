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

    public Keycard index;
    public void PickUp()
    {
        CharacterManager.Instance.Player.key[(int)index] = true;
        Destroy(gameObject);
    }
}
