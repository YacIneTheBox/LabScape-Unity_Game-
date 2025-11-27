using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class DroneChase : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform playerTarget;

    [Header("Flight Settings")]
    public float flightHeight = 2.0f;
    public float hoverSpeed = 2.0f;
    public float hoverAmplitude = 0.2f;
    public float rotationSpeed = 10.0f; // Controls how fast it turns to face you

    private NavMeshAgent _agent;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        // IMPORTANT: Disable the agent's auto-rotation so we can control it manually
        _agent.updateRotation = false; 

        // Safety check to ensure agent is on the mesh
        if (!_agent.isOnNavMesh)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(transform.position, out hit, 20.0f, NavMesh.AllAreas))
            {
                transform.position = hit.position;
                _agent.Warp(hit.position);
            }
        }
    }

    void Update()
    {
        if (playerTarget == null || !_agent.isActiveAndEnabled || !_agent.isOnNavMesh) return;

        // 1. Move Logic
        _agent.SetDestination(playerTarget.position);

        // 2. Float Logic
        float hoverEffect = Mathf.Sin(Time.time * hoverSpeed) * hoverAmplitude;
        _agent.baseOffset = flightHeight + hoverEffect;

        // 3. Rotation Logic (Always Face Player)
        FaceTarget();
    }

       void FaceTarget()
    {
        Vector3 direction = (playerTarget.position - transform.position).normalized;
        direction.y = 0; 

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // FIX: Add a rotation offset if the model is facing the wrong way.
            // If the drone is facing sideways (right), try 90 or -90.
            // If the drone is facing backwards, try 180.
            // Example: Quaternion.Euler(0, 90, 0) adds 90 degrees to the Y axis.
            Quaternion correction = Quaternion.Euler(0, -120, 0); // <--- ADJUST THIS NUMBER (0, 90, 180, -90)
            
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation * correction, Time.deltaTime * rotationSpeed);
        }
    }

}
