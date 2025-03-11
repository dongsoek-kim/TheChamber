using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
public class Oil : MonoBehaviour
{
    public float curValue;
    public float startValue;
    public float maxValue;
    public float passiveValue;
    public Image uiBar;
    public Image Runstate;

    private PlayerController playerController;
    private void Awake()
    {
    }
    private void Start()
    {
        playerController = CharacterManager.Instance.Player.controller;
        playerController.runStart += RunStart;
        playerController.runEnd += RunEnd;

        curValue = startValue;
    }

    private void Update()
    {
        uiBar.fillAmount = GetPercentage();
        if(curValue==0)
        {
            Death();
        }
    }

    float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void Add(float value)
    {
        curValue = Mathf.Min(curValue + value, maxValue);
    }

    public void Subtract(float value)
    {
        curValue = Mathf.Max(curValue - value, 0);
    }
    /// <summary>
    /// 달리면 더 빨리 소모
    /// </summary>
    public void RunStart()
    {
        passiveValue *= 2;
        Runstate.gameObject.SetActive(true);
    }
    public void RunEnd()
    {
        passiveValue /= 2;
        Runstate.gameObject.SetActive(false);
    }
    public void Death()
    {
        curValue = 100;
        CharacterManager.Instance.Player.controller.Restart();
    }
}
