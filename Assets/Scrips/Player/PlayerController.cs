using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour
{
    [Header("Moverment")]
    public float moveSpeed;
    private Vector2 curMovementInput;
    public float jumpPower;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCourXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;
    public bool canLook = true;

    public Action option;
    private Rigidbody _rigidbody;
    private bool isOnPlatform = false;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
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
        dir *= moveSpeed;
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
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
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
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }

    public void JumpPlatform(float jumpPower)
    {
        if (!isOnPlatform)  
        {
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
        moveSpeed = 5;
    }
    public void OnAccelerationPlatformExit()
    {
        moveSpeed = 3;
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
