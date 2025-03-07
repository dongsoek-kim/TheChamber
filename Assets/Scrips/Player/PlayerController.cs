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
    private float currentSpeed;
    [SerializeField]private bool isBackward = false;

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

    void FixedUpdate()
    {
        float targetSpeed = isRunning ? runSpeed : moveSpeed;
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

    void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= currentSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }

    void CameraLook()
    {
        camCourXRot += mouseDelta.y * lookSensitivity;
        camCourXRot = Mathf.Clamp(camCourXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCourXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            moveSpeed = walkSpeed;
            animator.SetBool("isMoving", true);
            
            curMovementInput = context.ReadValue<Vector2>();


            if (curMovementInput.y < 0)
            {
                moveSpeed = backSpeed;
                isBackward=true;
                animator.SetBool("isBackWard", true);
            }
            else
            {
                moveSpeed = walkSpeed;
                isBackward = false;
                animator.SetBool("isBackWard", false);
            }
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            // �̵��� ���߸� isMoving�� false�� ����
            animator.SetBool("isMoving", false);

            // �ڷ� �̵����� �ʴ� ���·� ����
            animator.SetBool("isBackWard", false);

            // �Է� �ʱ�ȭ
            curMovementInput = Vector2.zero;
        }
    }

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
    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
             animator.SetTrigger("isJumping");
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }

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

    public void AccelerationPlatform()
    {
        moveSpeed = 7;
        runSpeed = 10;
    }
    public void OnAccelerationPlatformExit()
    {
        moveSpeed = 2;
        runSpeed = 5;
    }

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

    public void OnOption(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            option?.Invoke();
            ToggleCursor();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        Camera camera;
        camera= Camera.main;
        float maxCheckDistance = 1;
        GameObject curInteractGameObject;
        if (context.phase == InputActionPhase.Started)
        {
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance))
            {
                if (hit.collider.gameObject.CompareTag("Access"))
                {
                    Door door = hit.collider.transform.parent.GetComponent<Door>();
                    door.DoorOpen();
                }
            }
        }
    }

    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}
