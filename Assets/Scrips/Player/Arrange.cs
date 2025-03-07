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

        
        if (Physics.Raycast(ray, out hit, arrangeDistance))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                if (hit.normal == Vector3.up)
                {                              
                    if (ghostObject == null)
                    {
                        ghostObject = Instantiate(CharacterManager.Instance.Player.itemData.ghostPrefab, hit.point, Quaternion.identity);
                    }
                    ghostObject.transform.position = new Vector3(
                        Mathf.RoundToInt(hit.point.x),  
                        Mathf.RoundToInt(hit.point.y-0.2f), 
                        Mathf.RoundToInt(hit.point.z) 
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
