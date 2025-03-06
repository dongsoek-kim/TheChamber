using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public Oil oil;
    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
        oil.Subtract(oil.passiveValue * Time.deltaTime);
    }
}
