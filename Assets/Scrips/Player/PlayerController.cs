using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    [Header("Moverment")]
    public float moveSpeed;
    public float walkSpeed;
    public float runSpeed;
    public float backSpeed;
    private Vector2 curMovementInput;
    public float jumpPower;
    public LayerMask groundLayerMask;
    public float accelerationTime = 0.5f;
    private bool isRunning = false;
    private bool isMoving= false;
    [SerializeField] private float currentSpeed;
    [SerializeField] private bool isBackward = false;
    private Coroutine waitUntilGroundedCoroutine;
    public bool canMove=true;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCourXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;
    public bool canLook = true;

    [Header("Animation")]
    public Animator animator;

    public Action option;
    public Action runStart;
    public Action runEnd;
    public Action reStart;
    private Rigidbody _rigidbody;
    private bool isOnPlatform = false;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// �������� ���� ������Ʈ
    /// Ű���� �Է½�, �޸������Ͻ� ���� ����ȯ
    /// </summary>
    void FixedUpdate()
    {
     
            float targetSpeed = isMoving ? (isRunning ? runSpeed : moveSpeed) : 0f;
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime / accelerationTime);
            Move();

    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    /// <summary>
    /// �÷��̾� �̵��� ���� �޼���
    /// </summary>
    void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= currentSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }

    /// <summary>
    /// ī�޶� ������ ���� �޼���
    /// </summary>
    void CameraLook()
    {
        camCourXRot += mouseDelta.y * lookSensitivity;
        camCourXRot = Mathf.Clamp(camCourXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCourXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    /// <summary>
    /// ����Ű �Է½� ����
    /// </summary>
    /// <param name="context"></param>
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed&&canMove)
        {
            moveSpeed = walkSpeed;
            animator.SetBool("isMoving", true);
            isMoving = true;
            curMovementInput = context.ReadValue<Vector2>();


            if (curMovementInput.y < 0)
            {
                moveSpeed = backSpeed;
                isBackward = true;
                animator.SetBool("isBackWard", true);
            }
            else
            {
                moveSpeed = walkSpeed;
                isBackward = false;
                animator.SetBool("isBackWard", false);
            }
        }
        else if (context.phase == InputActionPhase.Canceled||!canMove)
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("isBackWard", false);
            isMoving = false;
            curMovementInput = Vector2.zero;
        }
    }

    /// <summary>
    /// Shift�Է½� ����
    /// </summary>
    /// <param name="context"></param>
    public void OnRun(InputAction.CallbackContext context)
    {
        if (isBackward)
        {
            return;
        }
        if (context.phase == InputActionPhase.Performed)
        {
            animator.SetBool("isRun", true);
            isRunning = true;
            runStart?.Invoke();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            animator.SetBool("isRun", false);
            isRunning = false;
            runEnd?.Invoke();

        }
    }

    /// <summary>
    /// ���콺 ���Ͱ��� ��� ����
    /// </summary>
    /// <param name="context"></param>
    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// �����̽� �Է½� ����
    /// </summary>
    /// <param name="context"></param>

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded()&&canMove)
        {
            animator.SetTrigger("isJumping");
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// �����÷����� �ö����� ����
    /// </summary>
    /// <param name="jumpPower"></param>
    public void JumpPlatform(float jumpPower)
    {
        if (!isOnPlatform)
        {
            animator.SetTrigger("isJumping");
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
            isOnPlatform = true;
            Invoke("OnPlatformExit", 0.5f);
        }
    }

    public void OnPlatformExit()
    {
        isOnPlatform = false;
    }

    /// <summary>
    /// ���ӹ��ǿ� �ö����� ����
    /// </summary>
    public void AccelerationPlatform()
    {
        if (waitUntilGroundedCoroutine != null)
        {
            StopCoroutine(waitUntilGroundedCoroutine);
        }
        moveSpeed = 5;
        runSpeed = 15;
    }
    public void OnAccelerationPlatformExit()
    {
        waitUntilGroundedCoroutine=StartCoroutine(WaitUntilGrounded());
    }
    /// <summary>
    /// ���ӹ��ǿ��� �������� ���� ��� ������ �ӵ� ������ �޼���
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitUntilGrounded()
    {
        while (!IsGrounded())
        {
            yield return null;
        }
        moveSpeed = 2;
        runSpeed = 5;
        currentSpeed = runSpeed;

        waitUntilGroundedCoroutine = null;
    }

    /// <summary>
    /// �÷��̾  ���� �ִ��� �����ϴ� �޼���
    /// </summary>
    /// <returns></returns>
    bool IsGrounded()
    {

        Ray ray = new Ray(transform.position + (transform.up * 0.55f), Vector3.down);

        Debug.DrawRay(ray.origin,ray.direction*0.1f, Color.red, 1f);
        if (Physics.Raycast(ray, 0.1f, groundLayerMask))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// �� Ŭ���� �ɼ�â ��۸޼���
    /// </summary>
    /// <param name="context"></param>
    public void OnOption(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            option?.Invoke();
            ToggleCursor();
        }
    }

    /// <summary>
    /// Ű�е�� �����Ǵ� �޼���
    /// </summary>
    /// <param name="context"></param>
    public void OnInteract(InputAction.CallbackContext context)
    {
        Camera camera;
        camera= Camera.main;
        float maxCheckDistance = 20;
        if (context.phase == InputActionPhase.Started)
        {
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance))
            {
                if (hit.collider.gameObject.CompareTag("Access"))
                {
                    Debug.Log("�����");
                    Door door = hit.collider.transform.parent.GetComponent<Door>();
                    door.DoorOpen();
                }
            }
        }
    }
    /// <summary>
    /// �ʱ�ȭ�� �޼��� �÷��̾ ���߰� �ʱ���ġ�� �̵�
    /// </summary>
    public void Restart()
    {
        Dontmove();
        transform.position = Vector3.zero;
        Invoke("Canmove", 0.3f);
        reStart?.Invoke();  
    }
    public void Dontmove()
    {
        canMove = false;
        canLook = false;
        curMovementInput = Vector2.zero;
    }
    public void Canmove()
    {
        canMove = true;
        canLook = true;
    }

    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}
