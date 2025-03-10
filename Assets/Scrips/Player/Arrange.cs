using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Arrange : MonoBehaviour
{
    new private Camera camera;
    public float arrangeDistance;
    private GameObject ghostObject;
    public Transform displaytran;
    public float checkRate = 0.05f;
    private float lastCheckTime;
    void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if (CharacterManager.Instance.Player.itemData != null)
            if (Time.time - lastCheckTime > checkRate)
            {
                lastCheckTime = Time.time;
                RaycastHelper.ProcessRaycast(arrangeDistance, HandleGhostPlacement);
            }
    }
    private void OnEnable()
    {
        ChangeOfView.OnViewChanged += HandleViewChanged;
    }

    private void OnDisable()
    {
        ChangeOfView.OnViewChanged -= HandleViewChanged;
    }
    public void OnArrange(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && CharacterManager.Instance.Player.hand.NowEuqipped())
        {
            Instantiate(CharacterManager.Instance.Player.itemData.dropPrefab, ghostObject.transform.position, Quaternion.identity);
            CharacterManager.Instance.Player.hand.Arrange();
            CharacterManager.Instance.Player.itemData = null;
            Destroy(ghostObject);
        }
    }

    void HandleGhostPlacement(RaycastHit hit)
    {
        if (hit.collider.CompareTag("Ground") && hit.normal == Vector3.up)
        {
            if (ghostObject == null)
            {
                ghostObject = Instantiate(CharacterManager.Instance.Player.itemData.ghostPrefab, hit.point, Quaternion.identity);
            }
            ghostObject.transform.position = new Vector3(
                Mathf.RoundToInt(hit.point.x),
                Mathf.RoundToInt(hit.point.y - 0.2f),
                Mathf.RoundToInt(hit.point.z)
            );

                ghostObject.SetActive(true);

        }
        else
        {
                ghostObject.SetActive(false);
        }
    }
    private void HandleViewChanged(View newView)
    {
        if (newView == View.Tps)
        {
            arrangeDistance = 5f;
        }
        else
        {
            // 다른 뷰일 때 기본값 또는 원하는 값으로 설정
            arrangeDistance = 3f;
        }
    }
}
