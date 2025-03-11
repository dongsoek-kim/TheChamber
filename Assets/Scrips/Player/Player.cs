using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public bool[] keycard=new bool[3];
    public ItemData itemData;
    private bool isDeath = false;
    private TextMeshProUGUI text;
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
