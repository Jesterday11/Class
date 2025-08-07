
using Unity.VisualScripting;
using UnityEngine.InputSystem; //need for new input system
using UnityEngine;


using UnityEngine;
using UnityEngine.InputSystem;
public class FPController : MonoBehaviour
{
    [Header("Movement Settings")] // make inspector look pretty
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    [Header("Look Settings")]
    public Transform cameraTransform;
    public float lookSensitivity = 2f;
    public float verticalLookLimit = 90f; // limit how far player can look up and down

    [Header("Shooting")]
    public GameObject bulletPrefab; // prefab for the bullet
    public Transform gunPoint; // point where the bullet spawns
    public float bulletSpeed = 1000f; // speed of the bullet

    [Header("Crouch Settings")]
    public float crouchHeight = 0.5f; // height of the player when crouching    
    public float standHeight = 1.8f; // height of the player when standing
    public float crouchSpeed = 2f; // speed of the player when crouching
    public float originalMoveSpeed; // original move speed of the player

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity; // move player around screen
    private float verticalRotation = 0f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // locks the cursor to the center of the screen 
        Cursor.visible = false; // makes the cursor invisible

        controller = GetComponent<CharacterController>();
        originalMoveSpeed = moveSpeed; // store the original move speed of the player

        Cursor.lockState = CursorLockMode.Locked; // locks the cursor to the center of the screen
        Cursor.visible = false; // makes the cursor invisible
    }
    private void Update()
    {
        HandleMovement();
        HandleLook();
    }
    public void OnMovement(InputAction.CallbackContext context) // refers to movement action in input actions
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext context) // refers to look action in input actions
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context) // refers to jump action in input actions
    {
        if (context.performed && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // calculates jump velocity based on jump height and gravity
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (bulletPrefab != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, gunPoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.AddForce(gunPoint.forward * bulletSpeed); // adds force to the bullet in the direction of the gun point
                Destroy(bullet, 2f); // destroys the bullet after 2 seconds to prevent clutter 
            }
        }
    }
    public void HandleMovement()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward *
        moveInput.y;
        controller.Move(move * moveSpeed * Time.deltaTime);
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    public void HandleLook()
    {
        float mouseX = lookInput.x * lookSensitivity;
        float mouseY = lookInput.y * lookSensitivity;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookLimit,
        verticalLookLimit);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    public void OnCrouch(InputAction.CallbackContext context) // refers to crouch action in input actions
    {
        if (context.performed)
        {
            controller.height = crouchHeight; // change height of the player when crouching
            moveSpeed = crouchSpeed; // change speed of the player when crouching
        }
        else if (context.canceled)
        {
            controller.height = standHeight; // change height of the player when standing
            moveSpeed = originalMoveSpeed; // change speed of the player when standing
        }
    }
}