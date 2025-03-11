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
                RaycastController.ProcessRaycast(arrangeDistance, HandleGhostPlacement);
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

    /// <summary>
    /// 플레이어가 오른쪽클릭시 위치에 발판 설치
    /// </summary>
    /// <param name="context"></param>
    public void OnArrange(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && CharacterManager.Instance.Player.hand.NowEuqipped())
        {
            
            GameObject dropInstance = Instantiate(CharacterManager.Instance.Player.itemData.dropPrefab, ghostObject.transform.position, Quaternion.identity);

            // 만약 ghostObject가 moving platform 위에 생성되었다면 (즉, 부모가 있다면)
            if (ghostObject.transform.parent != null && ghostObject.transform.parent.CompareTag("MoivgPlatform"))
            {
                dropInstance.transform.SetParent(ghostObject.transform.parent);
            }
            
            CharacterManager.Instance.Player.hand.Arrange();
            CharacterManager.Instance.Player.itemData = null;
            Destroy(ghostObject);
        }
    }
    /// <summary>
    /// 플레이어 에임에 가장 가까운 바닥에
    /// 반투명한 플랫폼을 미리 위치하여
    /// 어디에 설치될지 보여줌
    /// </summary>
    /// <param name="hit"></param>
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
        else if ((hit.collider.CompareTag("MoivgPlatform") && hit.normal == Vector3.up))
        {
            if (ghostObject == null)
            {
                ghostObject = Instantiate(CharacterManager.Instance.Player.itemData.ghostPrefab, hit.point, Quaternion.identity);
                // ghostObject의 부모를 충돌한 moving platform으로 설정
                ghostObject.transform.SetParent(hit.collider.transform);
            }
            ghostObject.transform.position = new Vector3(
                Mathf.RoundToInt(hit.point.x),
                hit.point.y-0.5f,
                Mathf.RoundToInt(hit.point.z)
            );
            ghostObject.SetActive(true);
        }
        else
        {
            ghostObject.transform.SetParent(null);
            ghostObject.SetActive(false);
        }
    }

    /// <summary>
    /// Tps,Fps에 따른 검출 거리 변환
    /// </summary>
    /// <param name="newView"></param>
    private void HandleViewChanged(View newView)
    {
        if (newView == View.Tps)
        {
            arrangeDistance = 5f;
        }
        else
        {
            arrangeDistance = 3f;
        }
    }
}
