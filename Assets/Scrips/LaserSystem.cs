using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class LaserSystem : MonoBehaviour
{
    [Header("Laser")]
    public GameObject[] lasers;
    public float changeInterval = 1f;
    public float rotationSpeed = 2f;
    private Quaternion[] targetRotations;
    public bool LaserEnabled=true;

    [Header("Gate")]
    public GameObject exitDoor;
    public GameObject shortCut;

    [Header("Button")]
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance=5;
    [SerializeField]private bool isButton;
    public int tryCount = 0;
    public GameObject Button;

    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        // 시작 시 모든 레이저를 비활성화
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(false);
        }

        // 배열 초기화
        targetRotations = new Quaternion[lasers.Length];
        for (int i = 0; i < lasers.Length; i++)
        {
            targetRotations[i] = lasers[i].transform.localRotation;
        }
    }

    void Update()
    {
        if (LaserEnabled && Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            RaycastHelper.ProcessRaycast(maxCheckDistance, HandleLaserButton);
        }

        for (int i = 0; i < lasers.Length; i++)
        {
            if (lasers[i].activeSelf)
            {
                lasers[i].transform.localRotation = Quaternion.Slerp(
                    lasers[i].transform.localRotation,
                    targetRotations[i],
                    Time.deltaTime * rotationSpeed
                );
            }
        }
    }

    public void HandleLaserButton(RaycastHit hit)
    {
        if(hit.collider.CompareTag("LaserButton")&& LaserEnabled)
        {
            isButton = true;
            text.gameObject.SetActive(true);
        }
        else 
        {
            isButton = false;
            text.gameObject.SetActive(false);
        }
    }
    public void OnLaserButton(InputAction.CallbackContext context)
    {
        if (!isButton)
        {
            return;
        }
        LaserEnabled = false;
        StartCoroutine(MoveObjectToPosition(Button, new Vector3(Button.transform.position.x, 16f, Button.transform.position.z), 1f));
        StartCoroutine(MoveObjectToPosition(exitDoor, new Vector3(0, exitDoor.transform.position.y, exitDoor.transform.position.z), 1f));
        ActivateLasers();
        if (tryCount > 3) StartCoroutine(MoveObjectToPosition(shortCut, new Vector3(0, shortCut.transform.position.y, shortCut.transform.position.z), 1f));
    }
    private IEnumerator MoveObjectToPosition(GameObject obj, Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = obj.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            obj.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = targetPosition;
    }

    public void ActivateLasers()
    {
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(true);
        }
        StartCoroutine(UpdateRotationsRoutine());
    }


    private IEnumerator UpdateRotationsRoutine()
    {
        while (true)
        {
            for (int i = 0; i < lasers.Length; i++)
            {
                float randomX = Random.Range(-15f, 15f);
                float randomY = Random.Range(-15f, 15f);
                float randomZ = Random.Range(-195f, -165f);
                targetRotations[i] = Quaternion.Euler(randomX, randomY, randomZ);
            }
            yield return new WaitForSeconds(changeInterval);
        }
    }

    public void Reset()
    {
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(false);
        }
        StartCoroutine(MoveObjectToPosition(Button, new Vector3(Button.transform.position.x, 17.5f, Button.transform.position.z), 1f));
        StartCoroutine(MoveObjectToPosition(exitDoor, new Vector3(5, exitDoor.transform.position.y, exitDoor.transform.position.z), 1f));
        if (tryCount > 3) StartCoroutine(MoveObjectToPosition(shortCut, new Vector3(-5, shortCut.transform.position.y, shortCut.transform.position.z), 1f));
        LaserEnabled = true;
        tryCount++;
    }
}
