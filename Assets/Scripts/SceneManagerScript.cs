using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    [Header("Portal Settings")]
    public string targetSceneName = "Supermarket_WIP"; // Make sure this EXACT name matches your scene file
    public Vector3 targetPosition = Vector3.zero;
    public Vector3 targetRotation = Vector3.zero;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered portal from DetectiveHub to " + targetSceneName);
            LoadSupermarketScene();
        }
    }

    void LoadSupermarketScene()
    {
        // Simple scene loading - make sure scenes are in Build Settings
        SceneManager.LoadScene(targetSceneName);
    }
}