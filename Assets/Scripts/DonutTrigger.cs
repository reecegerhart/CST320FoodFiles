using UnityEngine;

public class DonutTrigger : MonoBehaviour
{
    // Reference to the main counter script (will be found in Start)
    private DonutCounter counterScript;

    // Flag to ensure the donut is only collected once
    private bool hasBeenCollected = false;

    void Start()
    {
        // Find the single instance of the DonutCounter script in the scene
        // We use FindObjectOfType because the counter script is likely on a GameManager or parent object.
        counterScript = FindFirstObjectByType<DonutCounter>();

        // Safety check
        if (counterScript == null)
        {
            Debug.LogError("DonutCounter script not found in the scene! Cannot track donut collection.");
        }
    }

    // Called when another Collider enters this trigger's space
    private void OnTriggerEnter(Collider other)
    {
        // 1. Check if the entering object is the Player AND the donut hasn't been collected
        // NOTE: Your Player GameObject MUST have the Tag "Player" set in the Inspector.
        if (other.CompareTag("Player") && !hasBeenCollected)
        {
            // 2. Mark as collected
            hasBeenCollected = true;

            // 3. Tell the main counter to increment the count
            if (counterScript != null)
            {
                counterScript.IncrementDonutCount();
            }

            // 4. Make the donut disappear visually
            // This is what removes the donut from the scene after the player walks over it.
            gameObject.SetActive(false);
        }
    }
}
