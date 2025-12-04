using UnityEngine;

public class WarehouseBackdoor : MonoBehaviour
{
    // Public variables for rotation settings
    // Adjust these values in the Inspector to control the opening animation
    public float openAngle = 90f;   // The final angle the door should open to (e.g., 90 degrees)
    public float rotationSpeed = 3f; // The speed of the opening animation

    private bool isOpening = false; // Flag to activate the rotation sequence
    private Quaternion startRotation;
    private Quaternion targetRotation;

    public GameObject exitPortalObject; // <--- NEW REFERENCE

    void Start()
    {
        startRotation = transform.rotation;

        // Calculate the final target rotation. Assumes rotation around the Y-axis.
        // If the door opens the wrong way, change 'openAngle' to a negative value (-90f) in the Inspector.
        targetRotation = startRotation * Quaternion.Euler(0, 0, openAngle);
    }

    void Update()
    {
        if (isOpening)
        {
            // Smoothly rotate the door from its current rotation towards the target rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            // Optional: Check if the door is fully open (stops the constant rotation calculation)
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                isOpening = false;
            }
        }
    }

    // This method is called EXTERNALLY by the BookPickup.cs script
    public void UnlockAndOpen()
    {
        Debug.Log("Warehouse Backdoor Unlocked! ESCAPE ROUTE OPEN!");

        // 1. Start the rotation process in the Update loop
        isOpening = true;

        // 2. Disable the door's collider so the player can walk through
        Collider doorCollider = GetComponent<Collider>();
        if (doorCollider != null)
        {
            doorCollider.enabled = false;
        }

        // 3. ACTIVATE THE EXIT PORTAL
        if (exitPortalObject != null)
        {
            exitPortalObject.SetActive(true);
            Debug.Log("Office Return Portal is now available!");
        }
    }

    // Optional: Provides a console message if the player tries to use the exit too early
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object is the Player and the door is not opening
        if (other.CompareTag("Player") && !isOpening)
        {
            Debug.Log("The back exit is locked. I need to find the files in the Office first!");
        }
    }
}