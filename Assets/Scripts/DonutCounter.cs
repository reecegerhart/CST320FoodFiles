using UnityEngine;

public class DonutCounter : MonoBehaviour
{
    // 1. PUBLIC VARIABLE: Drag your key object from the Hierarchy into this slot
    public GameObject warehouseKeyObject;

    private int donutCount = 0;
    private const int requiredDonuts = 8;

    void Start()
    {
        // Safety Check: Ensure the key is hidden at the very start of the game
        if (warehouseKeyObject != null && warehouseKeyObject.activeSelf)
        {
            warehouseKeyObject.SetActive(false);
        }
    }

    // Called by the DonutTrigger when a donut is collected
    public void IncrementDonutCount()
    {
        donutCount++;

        if (donutCount >= requiredDonuts)
        {
            ShowKeyHint();
        }
        else
        {
            // Simple log to track progress in the Console
            Debug.Log($"Donuts collected: {donutCount} / {requiredDonuts}");
        }
    }

    private void ShowKeyHint()
    {
        // 1. Activate the key in the scene (This makes it appear in the bathroom)
        if (warehouseKeyObject != null)
        {
            warehouseKeyObject.SetActive(true);
            Debug.Log("--- HINT LOGIC TRIGGERED --- Warehouse Key Activated in the scene!");
            Debug.Log("HINT: You've followed the trace! Check the Bathroom!");
        }

        // 2. Stop the counter
        enabled = false;
    }
}