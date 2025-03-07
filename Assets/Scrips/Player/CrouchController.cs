using System.Collections;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CrouchController : MonoBehaviour
{
    public Player player;
    private Animator animator;
    public float crouchMoveDuration = 3f;
    private Vector3 crouchTargetPosition;
    private bool isAbleToCrouch = false;
    void Start()
    {
        player = CharacterManager.Instance.Player;
        animator = player.controller.animator;
    }
    private void Update()
    {
        Vector3 rayOrigin = player.transform.position + new Vector3(0, 2.5f, 0);
        Debug.DrawRay(rayOrigin, player.transform.forward * 0.5f, UnityEngine.Color.red);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (other.bounds.Intersects(other.bounds))
            {
                Debug.Log("충돌테스트 통과");
                float targetY = other.bounds.max.y;
                isAbleToCrouch = true;
                crouchTargetPosition = new Vector3(transform.position.x, targetY, transform.position.z);
                Debug.Log(crouchTargetPosition);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        isAbleToCrouch = false;
        Debug.Log("캐릭터움직임");
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Vector3 rayOrigin = player.transform.position + new Vector3(0, 2.5f, 0);
            if (Physics.Raycast(rayOrigin, player.transform.forward, out RaycastHit hit, 0.5f))
            {
                Debug.Log("충돌테스트 실패");
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


