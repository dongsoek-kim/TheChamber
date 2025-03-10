using System.Collections;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CrouchController : MonoBehaviour
{
    public Player player;
    private Animator animator;
    public float crouchMoveDuration = 4f;
    private Vector3 crouchTargetPosition;
    private bool isAbleToCrouch = false;


    void Start()
    {
        player = CharacterManager.Instance.Player;
        animator = player.controller.animator;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (other.bounds.Intersects(other.bounds))
            {
                float targetY = other.bounds.max.y;
                isAbleToCrouch = true;
                crouchTargetPosition = new Vector3(transform.position.x, targetY, transform.position.z);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        isAbleToCrouch = false;
    }
    /// <summary>
    /// C를 누르면 기어오르기 실행, 레이를 통해 충돌여부 판정
    /// </summary>
    /// <param name="context"></param>
    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Vector3 rayOrigin = player.transform.position + new Vector3(0, 2.5f, 0);
            if (Physics.Raycast(rayOrigin, player.transform.forward, out RaycastHit hitfoward, 0.5f))
            {
                Debug.Log("앞에 물건있어서 못올라감");
                return;
            }
            else if (Physics.Raycast(rayOrigin, player.transform.up, out RaycastHit hitup, 1f))
            {
                Debug.Log("위에 물건 있어서 못올라감");
                return;
            }
            else if (Physics.Raycast(rayOrigin + new Vector3(0, 1f, 0), -player.transform.up, out RaycastHit hitDown, 1f))
            {
                Debug.Log("위에 물건 있어서 못올라감");
                return;
            }
            if (isAbleToCrouch)
            {
                isAbleToCrouch = false;
                animator.SetTrigger("isCrouch"); 
                StartCoroutine(MovePlayerSmooth(crouchTargetPosition, crouchMoveDuration));
            }
        }
    }
    private IEnumerator MovePlayerSmooth(Vector3 target, float duration)
    {
        Vector3 startPos = player.transform.position;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            player.transform.position = Vector3.Lerp(startPos, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}


