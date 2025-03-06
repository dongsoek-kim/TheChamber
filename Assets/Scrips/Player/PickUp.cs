using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUp : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    public GameObject curInteractGameObject;
    private HandleItem curInteractable;

    new private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<HandleItem>();
                }
            }
            else
            {
                curInteractGameObject = null;
                curInteractable = null;
            }
        }
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        Debug.Log("��ȣ�ۿ� ������");
        if (context.phase == InputActionPhase.Started && curInteractable != null && !CharacterManager.Instance.Player.hand.NowEuqipped())
        {
            Debug.Log("���� �����");
            curInteractable.PickUp();
            CharacterManager.Instance.Player.hand.Equip(CharacterManager.Instance.Player.itemData);
            curInteractGameObject = null;
            curInteractable = null;
        }
        else if (CharacterManager.Instance.Player.hand.NowEuqipped())
            Debug.Log("�տ� �����ִ�");
        else
        {
            Debug.Log("����̴�");
        }
    }
}
