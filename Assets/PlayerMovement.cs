using UnityEngine;
using UnityEngine.InputSystem; // New Input System

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    public float jumpForce = 5f;
    public Transform cameraTransform; // Drag your camera here
    public Transform feetTransform;  // Drag the feet mesh here
    public float groundCheckRadius = 0.3f;

    private float cameraPitch = 0f;
    private Rigidbody rb;
    private bool isGrounded;

    // Layer mask to ignore the player layer
    private int ignorePlayerLayerMask;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent tipping over

        // Create a mask that includes everything except the Player layer
        int playerLayer = gameObject.layer;
        ignorePlayerLayerMask = ~(1 << playerLayer);
    }

    void Update()
    {
        // --------- MOVEMENT INPUT ----------
        Vector2 moveInput = Vector2.zero;

        if (Keyboard.current.wKey.isPressed) moveInput.y += 1;
        if (Keyboard.current.sKey.isPressed) moveInput.y -= 1;
        if (Keyboard.current.aKey.isPressed) moveInput.x -= 1;
        if (Keyboard.current.dKey.isPressed) moveInput.x += 1;

        if (Keyboard.current.upArrowKey.isPressed) moveInput.y += 1;
        if (Keyboard.current.downArrowKey.isPressed) moveInput.y -= 1;
        if (Keyboard.current.leftArrowKey.isPressed) moveInput.x -= 1;
        if (Keyboard.current.rightArrowKey.isPressed) moveInput.x += 1;

        if (moveInput.magnitude > 1f)
            moveInput.Normalize();

        // --------- CAMERA LOOK ----------
        Vector2 mouseDelta = Mouse.current.delta.ReadValue() * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseDelta.x);

        cameraPitch -= mouseDelta.y;
        cameraPitch = Mathf.Clamp(cameraPitch, -80f, 80f);
        cameraTransform.localEulerAngles = new Vector3(cameraPitch, 0f, 0f);

        // --------- JUMP INPUT ----------
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // --------- MOVEMENT ----------
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = (forward * moveInput.y + right * moveInput.x).normalized;
        Vector3 velocity = moveDir * moveSpeed;
        velocity.y = rb.linearVelocity.y; // keep gravity/jump intact
        rb.linearVelocity = velocity;
    }

    void FixedUpdate()
    {
        if (feetTransform != null)
        {
            // Sphere check ignores player layer
            isGrounded = Physics.CheckSphere(feetTransform.position, groundCheckRadius, ignorePlayerLayerMask);

            // Debug: shows green if grounded, red if not
            Debug.DrawRay(feetTransform.position, Vector3.up * 0.1f, isGrounded ? Color.green : Color.red);
        }
    }
}
