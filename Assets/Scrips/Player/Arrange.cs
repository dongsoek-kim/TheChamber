using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Arrange : MonoBehaviour
{
    new private Camera camera;
    public float arrangeDistance;
    void Start()
    {
        camera = Camera.main;
    }
    public void OnArrange(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && CharacterManager.Instance.Player.hand.NowEuqipped())
        {
            CharacterManager.Instance.Player.hand.Arrange();

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, arrangeDistance))
            {
                // 충돌한 오브젝트의 태그가 "Ground"인지 확인
                if (hit.collider.CompareTag("Ground"))
                {
                    Debug.Log("Ray hit the ground!");
                }
            }
            
            CharacterManager.Instance.Player.hand.Arrange();
            CharacterManager.Instance.Player.itemData = null;
        }
    }
}
