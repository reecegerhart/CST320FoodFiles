using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    // Inventory Flags (Global Access)
    public static bool HasWarehouseKey = false;
    public static bool HasOfficeKey = false; // <-- NEW FLAG for the second key

    // Inspector Variable: Set this in the Inspector for each key object
    public bool IsOfficeKey = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (IsOfficeKey)
            {
                HasOfficeKey = true; // Set the Office Key flag
                Debug.Log("--- Office Key Acquired! ---");
            }
            else
            {
                HasWarehouseKey = true; // Set the Warehouse Key flag
                Debug.Log("--- Warehouse Key Acquired! ---");
            }

            // Key disappears visually for both types
            gameObject.SetActive(false);

            // Clean up the script instance
            Destroy(this);
        }
    }
}