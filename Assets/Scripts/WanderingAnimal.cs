using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class WanderingAnimal : MonoBehaviour
{
    [Header("Animal Settings")]
    public AnimalType animalType = AnimalType.Cow;
    
    [Header("Wandering Settings")]
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;
    public float minWanderDistance = 3f;
    
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float rotationSpeed = 120f;
    
    [Header("Detection Settings")]
    public float detectionDistance = 3f;
    public LayerMask obstacleLayer = 1;
    
    // Enum for different animal types
    public enum AnimalType
    {
        Cow,
        Sheep,
        Chicken,
        Pig,
        Horse,
        Custom
    }

    private NavMeshAgent agent;
    private float timer;
    private Vector3 currentDestination;
    private bool isWandering = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            agent = gameObject.AddComponent<NavMeshAgent>();
        }
        
        // Set up the agent based on animal type
        ConfigureAnimalByType();
        
        timer = wanderTimer;
    }

    void ConfigureAnimalByType()
    {
        switch (animalType)
        {
            case AnimalType.Cow:
                agent.speed = 1.5f;
                agent.angularSpeed = 100f;
                wanderRadius = 15f;
                break;
            case AnimalType.Sheep:
                agent.speed = 2f;
                agent.angularSpeed = 150f;
                wanderRadius = 8f;
                break;
            case AnimalType.Chicken:
                agent.speed = 1.2f;
                agent.angularSpeed = 200f;
                wanderRadius = 5f;
                wanderTimer = 3f; // Chickens change direction more often
                break;
            case AnimalType.Pig:
                agent.speed = 1f;
                agent.angularSpeed = 90f;
                wanderRadius = 6f;
                break;
            case AnimalType.Horse:
                agent.speed = 3f;
                agent.angularSpeed = 120f;
                wanderRadius = 20f;
                break;
            case AnimalType.Custom:
                // Use the inspector values
                agent.speed = moveSpeed;
                agent.angularSpeed = rotationSpeed;
                break;
        }
        
        agent.acceleration = 8f;
        agent.stoppingDistance = 0.5f;
    }

    void Update()
    {
        if (!isWandering || !agent.enabled) return;
        
        timer += Time.deltaTime;

        if (timer >= wanderTimer || agent.remainingDistance < 0.5f)
        {
            Vector3 newDestination = GetRandomDestination();
            if (newDestination != Vector3.zero)
            {
                agent.SetDestination(newDestination);
                currentDestination = newDestination;
            }
            timer = 0;
        }

        CheckForObstacles();
    }

    Vector3 GetRandomDestination()
    {
        for (int i = 0; i < 10; i++) // Try up to 10 times to find a valid position
        {
            Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
            randomDirection += transform.position;
            randomDirection.y = transform.position.y; // Keep same height
            
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, NavMesh.AllAreas))
            {
                float distance = Vector3.Distance(transform.position, hit.position);
                if (distance >= minWanderDistance)
                {
                    return hit.position;
                }
            }
        }
        
        // If no valid position found, just wander a short distance
        return transform.position + transform.forward * minWanderDistance;
    }

    void CheckForObstacles()
    {
        RaycastHit hit;
        Vector3 forward = transform.forward;
        
        if (Physics.Raycast(transform.position, forward, out hit, detectionDistance, obstacleLayer))
        {
            AvoidObstacle(hit);
        }
        
        // Additional check for obstacles on sides
        CheckSideObstacles();
    }

    void CheckSideObstacles()
    {
        RaycastHit hit;
        float sideAngle = 30f; // Check 30 degrees to each side
        
        // Check left
        Vector3 leftDirection = Quaternion.Euler(0, -sideAngle, 0) * transform.forward;
        if (Physics.Raycast(transform.position, leftDirection, out hit, detectionDistance * 0.7f, obstacleLayer))
        {
            AvoidObstacle(hit);
        }
        
        // Check right
        Vector3 rightDirection = Quaternion.Euler(0, sideAngle, 0) * transform.forward;
        if (Physics.Raycast(transform.position, rightDirection, out hit, detectionDistance * 0.7f, obstacleLayer))
        {
            AvoidObstacle(hit);
        }
    }

    void AvoidObstacle(RaycastHit hit)
    {
        Vector3 avoidanceDirection = GetAvoidanceDirection(hit.normal);
        Vector3 newDestination = transform.position + avoidanceDirection * minWanderDistance;
        
        NavMeshHit navHit;
        if (NavMesh.SamplePosition(newDestination, out navHit, minWanderDistance, NavMesh.AllAreas))
        {
            agent.SetDestination(navHit.position);
            timer = wanderTimer;
        }
    }

    Vector3 GetAvoidanceDirection(Vector3 obstacleNormal)
    {
        Vector3[] directions = {
            Vector3.Reflect(transform.forward, obstacleNormal).normalized,
            Quaternion.Euler(0, 45, 0) * obstacleNormal,
            Quaternion.Euler(0, -45, 0) * obstacleNormal,
            transform.right,
            -transform.right
        };

        foreach (Vector3 direction in directions)
        {
            if (!Physics.Raycast(transform.position, direction, detectionDistance, obstacleLayer))
            {
                return direction;
            }
        }
        
        return -transform.forward; // Back up if all else fails
    }

    // Public methods
    public void StopWandering()
    {
        isWandering = false;
        if (agent != null && agent.enabled)
            agent.isStopped = true;
    }

    public void ResumeWandering()
    {
        isWandering = true;
        if (agent != null && agent.enabled)
            agent.isStopped = false;
    }

    public void SetWanderParameters(float newRadius, float newTimer, float newSpeed)
    {
        wanderRadius = newRadius;
        wanderTimer = newTimer;
        agent.speed = newSpeed;
    }

    // Visual debugging
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
        
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * detectionDistance);
        
        // Draw side detection rays
        Gizmos.color = Color.blue;
        Vector3 leftDir = Quaternion.Euler(0, -30, 0) * transform.forward;
        Vector3 rightDir = Quaternion.Euler(0, 30, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, leftDir * detectionDistance * 0.7f);
        Gizmos.DrawRay(transform.position, rightDir * detectionDistance * 0.7f);
        
        if (Application.isPlaying && agent != null && agent.hasPath)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, agent.destination);
            Gizmos.DrawWireSphere(agent.destination, 0.3f);
        }
    }
}