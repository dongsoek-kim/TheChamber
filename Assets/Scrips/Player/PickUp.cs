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

            if (Physics.Raycast(ray, out hit, maxCheckDistance))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IItem>();
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
}
