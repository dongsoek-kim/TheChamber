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
                Ghosting();
                Vector3 rayOrigin = ghostObject.transform.position+new Vector3(0, 0.5f, 0); 
                Debug.Log(ghostObject.transform.position);
                Physics.Raycast(rayOrigin, ghostObject.transform.up, out RaycastHit uphit, 0.2f);
                Debug.DrawRay(rayOrigin, ghostObject.transform.up * 0.5f, Color.red);
            }
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
    public void Ghosting()
    {

        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // Raycast로 충돌 확인
        if (Physics.Raycast(ray, out hit, arrangeDistance))
        {
            // 충돌한 오브젝트가 "Ground"인 경우
            if (hit.collider.CompareTag("Ground"))
            {
                // 윗면(지면)에 충돌한 경우
                if (hit.normal == Vector3.up)
                {
                    
                    
                        // 미리보기 아이템이 없다면 생성
                    if (ghostObject == null)
                    {
                        ghostObject = Instantiate(CharacterManager.Instance.Player.itemData.ghostPrefab, hit.point, Quaternion.identity);
                    }
                    Vector3 rayOrigin = ghostObject.transform.position + new Vector3(0, 0.5f, 0);
                    if (Physics.Raycast(rayOrigin, ghostObject.transform.up, out RaycastHit uphit, 0.2f))
                    {
                        Debug.DrawRay(rayOrigin, ghostObject.transform.up*0.5f, Color.red);
                        Debug.Log("위에 물건이있어요");
                    }
                    // 미리보기 아이템을 충돌 위치에 업데이트
                    ghostObject.transform.position = new Vector3(
                        Mathf.RoundToInt(hit.point.x),  // X좌표를 정수로 반올림
                        Mathf.RoundToInt(hit.point.y-0.2f),  // Y좌표를 정수로 반올림
                        Mathf.RoundToInt(hit.point.z)   // Z좌표를 정수로 반올림
                    );
                    ghostObject.SetActive(true);
                }
            }
        }
        else
        {
            // 레이가 지면에 닿지 않으면 미리보기 아이템을 비활성화
            if (ghostObject != null)
            {
                ghostObject.SetActive(false);
            }
        }
    }
}
