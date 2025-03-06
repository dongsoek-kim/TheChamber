using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    void PickUp();
    void Drop();
}

public abstract class HandleItem : MonoBehaviour, IItem
{
    public void PickUp()
    {
        //��� �տ� ���
    }

    public void Drop()
    {
        //���տ� ��������
    }
}

public abstract class UsableItem : MonoBehaviour, IItem
{
    public void PickUp()
    {
    }
    public void Drop() { }//������
    public abstract void Use(float value);
}
