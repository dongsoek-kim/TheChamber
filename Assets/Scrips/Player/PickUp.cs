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
    /// ���ӿ� �ִ� ������ IITem�������̽��� ���� �ƴϸ� null��ȯ
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
    /// Handel,KeyCard ���̾ ���� ����
    /// Hamdel �� ��� �տ� ����
    /// KeyCard �� ��� �κ��丮 ����
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
    /// Tps,Fps������ �Ÿ� ��ȯ
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
