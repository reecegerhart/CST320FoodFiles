using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.name);
    }
}

