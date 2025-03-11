using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// IItem아이템 인터페이스
/// </summary>
public interface IItem
{
    void PickUp();
}
/// <summary>
/// HandleItem 부모
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
/// KeyCard 부모
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

