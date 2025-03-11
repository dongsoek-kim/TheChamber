using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// IItem������ �������̽�
/// </summary>
public interface IItem
{
    void PickUp();
}
/// <summary>
/// HandleItem �θ�
/// </summary>
public abstract class HandleItem : MonoBehaviour, IItem
{
    public ItemData data;


    public void PickUp()
    {
        CharacterManager.Instance.Player.itemData = data;
        Destroy(gameObject);
    }
}
/// <summary>
/// KeyCard �θ�
/// </summary>
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

