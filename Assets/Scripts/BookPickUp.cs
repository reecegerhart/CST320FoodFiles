using UnityEngine;

public class BookPickup : MonoBehaviour
{
    // Inventory Flag (optional now, but good practice)
    public static bool HasInventoryBook = false;

    // PUBLIC REFERENCE: Drag the Warehouse Backdoor object here in the Inspector
    public WarehouseBackdoor warehouseBackdoorScript;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object is the Player and the book hasn't been picked up
        if (other.CompareTag("Player") && !HasInventoryBook)
        {
            // 1. "Grab" the book
            HasInventoryBook = true;

            // 2. Make the book disappear visually
            gameObject.SetActive(false);

            // 3. TRIGGER THE ESCAPE DOOR OPENING
            if (warehouseBackdoorScript != null)
            {
                warehouseBackdoorScript.UnlockAndOpen();
                Debug.Log("--- Inventory Book Acquired! --- Escape route opened!");
            }

            // Optional: Destroy the script instance
            Destroy(this);
        }
    }
}