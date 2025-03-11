using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUp : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;

    public GameObject curInteractGameObject;
    private IItem curInteractable;

    new private Camera camera;

    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {

        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            RaycastController.ProcessRaycast(maxCheckDistance, HandleInteraction);
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
    /// 에임에 있는 물건이 IITem인터페이스면 저장 아니면 null반환
    /// </summary>
    /// <param name="hit"></param>
    void HandleInteraction(RaycastHit hit)
    {
        if (hit.collider.gameObject != curInteractGameObject)
        {
            curInteractGameObject = hit.collider.gameObject;
            curInteractable = hit.collider.GetComponent<IItem>();
        }
        else
        {
            curInteractGameObject = null;
            curInteractable = null;
        }
    }

    /// <summary>
    /// Handel,KeyCard 레이어에 따른 동작
    /// Hamdel 일 경우 손에 장착
    /// KeyCard 일 경우 인벤토리 습득
    /// </summary>
    /// <param name="context"></param>
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (curInteractable == null)
        {
            return;
        }

        GameObject targetObject = curInteractGameObject; 
        string targetLayer = LayerMask.LayerToName(targetObject.layer);
        if (context.phase == InputActionPhase.Started && curInteractable != null && targetLayer == "Handel")
        {
            if (!CharacterManager.Instance.Player.hand.NowEuqipped())
                curInteractable.PickUp();
            CharacterManager.Instance.Player.hand.Equip(CharacterManager.Instance.Player.itemData);
            curInteractGameObject = null;
            curInteractable = null;
        }
        else if (context.phase == InputActionPhase.Started && curInteractable != null && targetLayer == "KeyCard")
        {
            curInteractable.PickUp();
            curInteractGameObject = null;
            curInteractable = null;
        }
    }
    /// <summary>
    /// Tps,Fps에따른 거리 변환
    /// </summary>
    /// <param name="newView"></param>
    private void HandleViewChanged(View newView)
    {
        if (newView == View.Tps)
        {
            maxCheckDistance = 5f;
        }
        else
        {
            maxCheckDistance = 3;
        }
    }

}
