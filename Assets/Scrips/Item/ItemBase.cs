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
        //집어서 손에 들기
    }

    public void Drop()
    {
        //눈앞에 내려놓기
    }
}

public abstract class UsableItem : MonoBehaviour, IItem
{
    public void PickUp()
    {
    }
    public void Drop() { }//사용안함
    public abstract void Use(float value);
}
