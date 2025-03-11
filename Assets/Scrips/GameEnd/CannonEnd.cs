using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CannonEnd : MonoBehaviour
{
    [Header("FindPlayer")]
    public TextMeshPro text;
    private bool isNearCannon = false;
    
    [Header("EndPos")]
    Player player;
    public Camera endcamera;
    public Transform startPos;
    public Transform endPos;
    public Image image;
    public TextMeshProUGUI endtext;

    [Header("Ending")]
    private float fadeDuration = 1.0f;
    private bool isFinished = false;

    public void Start()
    {
        player = CharacterManager.Instance.Player;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearCannon = true;
            text.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearCannon = false;
            text.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (isNearCannon && Input.GetKeyDown(KeyCode.E))
        {
            player.controller.Dontmove();
            endcamera.gameObject.SetActive(true);
            endcamera.transform.position = new Vector3(
                player.transform.position.x,
                player.transform.position.y + 2f,
                player.transform.position.z
                );
            endcamera.transform.rotation = player.transform.rotation;
            player.transform.position = Vector3.zero;
            Invoke("CoroutineStart", 1f);

        }

        if (isFinished && Input.anyKeyDown)
        {
            Debug.Log("종료");
            Application.Quit();
        }
    }
    void CoroutineStart()
    {
        StartCoroutine(OnCannonEnd());
    }
    public IEnumerator OnCannonEnd()
    {
        float moveDuration = 3f;
        float elapsedTime = 0f;
        Vector3 initialPosition = endcamera.transform.position;
        Vector3 startPosition = startPos.position;
        Vector3 endPosition = endPos.position;
        Quaternion initialRotation = endcamera.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0f, 90f, 0f);

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            endcamera.transform.position = Vector3.Lerp(initialPosition, startPosition, elapsedTime / moveDuration);    

            yield return null;
        }

        endcamera.transform.position = startPosition;


        moveDuration = 1f;
        elapsedTime = 0f;
        float angleDifference = Quaternion.Angle(initialRotation, targetRotation);
        float rotationSpeed = angleDifference / moveDuration;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float step = rotationSpeed * Time.deltaTime;
            endcamera.transform.Rotate(Vector3.up * step);
            if (Quaternion.Angle(endcamera.transform.rotation, targetRotation) < 1f)
            {
                break;
            }

            yield return null;
        }

        endcamera.transform.rotation = targetRotation;

        moveDuration = 1f;
        elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            endcamera.transform.position = Vector3.Lerp(initialPosition, endPosition, elapsedTime / moveDuration);

            yield return null;
        }
        elapsedTime = 0f;
        Color initialColor = image.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration); // alpha 값 변화
            image.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            yield return null;
        }

        image.color = Color.black;

        endtext.gameObject.SetActive(true);

        isFinished = true;
    }
}
