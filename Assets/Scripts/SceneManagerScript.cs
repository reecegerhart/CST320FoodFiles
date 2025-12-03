using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public string sceneToLoad = "Scene2"; // Set this in Inspector
    
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the player
        if (other.CompareTag("Player"))
        {
            // Load the next scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}

