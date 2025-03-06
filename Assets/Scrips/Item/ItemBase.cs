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
        Debug.Log("플레이어에게 정보전달");
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
        CharacterManager.Instance.GetCard(index);
        Destroy(gameObject);
    }
}
