using UnityEngine;

public class OfficeDoor : MonoBehaviour
{
    // Public variables for rotation settings
    public float openAngle = 90f;   // How far the door should open (e.g., 90 degrees)
    public float rotationSpeed = 3f; // Speed of the opening animation

    private bool isOpening = false; // Flag to start the rotation
    private Quaternion startRotation;
    private Quaternion targetRotation;


    void Start()
    {
        startRotation = transform.rotation;
        // Calculate the target rotation (openAngle degrees around the Z-axis)
        targetRotation = startRotation * Quaternion.Euler(0, 0, openAngle);
    }

    // Use Update to execute the smooth rotation over several frames
    void Update()
    {
        if (isOpening)
        {
            // Smoothly rotate the door from its current rotation towards the target rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            // Optional: Stop opening when it's very close to the target angle
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                isOpening = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // CHECK THE NEW OFFICE KEY FLAG
            if (KeyPickup.HasOfficeKey)
            {
                OpenDoor();
            }
            else
            {
                Debug.Log("The Office Door is locked. I need the Office Key, which might be somewhere deeper in the supermarket!");
            }
        }
    }

    private void OpenDoor()
    {
        Debug.Log("Warehouse Unlocked! Door Opening!");

        // Start the rotation process in the Update loop
        isOpening = true;

        // Disable the collider so the player can walk through
        GetComponent<Collider>().enabled = false;

        // Optional: If you want the door to be destroyed *after* it opens:
        // Destroy(gameObject, 5f); // Destroys the door 5 seconds later
    }
}