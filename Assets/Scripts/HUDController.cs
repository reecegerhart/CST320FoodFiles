using UnityEngine;
using TMPro; // Make sure this matches your TextMeshPro setup!

public class HintUIController : MonoBehaviour
{
    private TextMeshProUGUI hintText;
    private float hideDelay = 10f; // Message stays on screen for 10 seconds

    void Awake()
    {
        // Get the TextMeshPro component attached to this same GameObject
        hintText = GetComponent<TextMeshProUGUI>();

        // Ensure the text starts hidden (though we set this in the Inspector too)
        gameObject.SetActive(false);
    }

    // Called by the DonutCounter to display the main hint
    public void DisplayKeyHint(string message)
    {
        if (hintText == null) return;

        // Set the text content and make the UI visible
        hintText.text = message;
        gameObject.SetActive(true);

        // Optional: Make the text stand out (e.g., color red)
        hintText.color = Color.red;

        // Cancel any pending hide call (just in case) and then schedule the message to disappear
        CancelInvoke("HideMessage");
        Invoke("HideMessage", hideDelay);
    }

    // Called by the KeyPickup script to show a brief confirmation
    public void DisplayConfirmation(string message, float duration = 3f)
    {
        if (hintText == null) return;

        hintText.text = message;
        hintText.color = Color.green; // Confirmation is usually green
        gameObject.SetActive(true);

        CancelInvoke("HideMessage");
        Invoke("HideMessage", duration);
    }

    private void HideMessage()
    {
        gameObject.SetActive(false);
    }
}