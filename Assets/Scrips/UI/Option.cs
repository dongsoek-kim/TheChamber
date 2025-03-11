using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class Option : MonoBehaviour
{

    private PlayerController controller;
    public GameObject optionWindow;
    public Button Changeofviewpoint;
    public Image[] cardImage;
    void Start()
    {

        controller = CharacterManager.Instance.Player.controller;
        optionWindow.SetActive(false);
        controller.option += Toggel;
        CharacterManager.Instance.getCard += UpdataKeycard;
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

    public void UpdataKeycard(int index)
    {
        Debug.Log(index);
        cardImage[index].gameObject.SetActive(true);
    }

}
