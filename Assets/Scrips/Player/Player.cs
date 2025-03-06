using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public Oil oil;
    public Handle hand;

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
