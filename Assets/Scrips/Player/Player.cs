using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Keycard
{
    Blue,
    Green,
    Red
}

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public Oil oil;
    public Handle hand;
    public bool[] key=new bool[3];
    public ItemData itemData;
    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        hand = GetComponent<Handle>();
    }

    private void Update()
    {
        oil.Subtract(oil.passiveValue * Time.deltaTime);
    }
}
