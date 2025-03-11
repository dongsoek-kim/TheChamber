using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoorEnd : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI text;
    private float fadeDuration = 1.0f;
    private bool isFinished = false;

    void Update()
    {
        if (isFinished && Input.anyKeyDown) 
        {
            Debug.Log("종료");
            Application.Quit();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            CharacterManager.Instance.Player.controller.Dontmove();
            StartCoroutine(TriggerDoorEnding());
        }
    }

    private IEnumerator TriggerDoorEnding()
    {
        float elapsedTime = 0f;
        Color initialColor = image.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration); // alpha 값 변화
            image.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            yield return null;
        }

        image.color = Color.black;

        text.gameObject.SetActive(true);

        isFinished = true;
    }
}