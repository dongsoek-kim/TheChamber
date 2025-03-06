using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Option : MonoBehaviour
{

    private PlayerController controller;

    public GameObject optionWindow;
    void Start()
    {

        controller = CharacterManager.Instance.Player.controller;
        optionWindow.SetActive(false);
        controller.option += Toggel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Toggel()
    {
        if (IsOpen())
        {
            optionWindow.SetActive(false);
        }
        else
        {
            optionWindow.SetActive(true);
        }
    }

    public bool IsOpen()
    {
        return optionWindow.activeInHierarchy;
    }
}
