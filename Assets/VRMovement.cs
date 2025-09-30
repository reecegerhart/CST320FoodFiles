using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleVRJoystickMovement : MonoBehaviour
{
    public Transform vrCamera; // Assign your Main Camera here
    public InputAction moveAction; // Assign this in Inspector or via script
    public float moveSpeed = 1.5f;

    private void OnEnable()
    {
        moveAction.Enable(); // Make sure the input action is enabled
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }

    void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        if (input == Vector2.zero) return;

        // Move relative to where the headset is facing (on XZ plane)
        Vector3 forward = new Vector3(vrCamera.forward.x, 0, vrCamera.forward.z).normalized;
        Vector3 right = new Vector3(vrCamera.right.x, 0, vrCamera.right.z).normalized;

        Vector3 move = (forward * input.y + right * input.x) * moveSpeed * Time.deltaTime;
        transform.position += move;
    }
}
