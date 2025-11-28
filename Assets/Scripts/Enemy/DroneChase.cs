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
    public float rotationSpeed = 10.0f; 

    private NavMeshAgent _agent;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        _agent.updateRotation = false; 

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

        _agent.SetDestination(playerTarget.position);

        float hoverEffect = Mathf.Sin(Time.time * hoverSpeed) * hoverAmplitude;
        _agent.baseOffset = flightHeight + hoverEffect;

      
        FaceTarget();
    }

       void FaceTarget()
    {
        Vector3 direction = (playerTarget.position - transform.position).normalized;
        direction.y = 0; 

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            
            Quaternion correction = Quaternion.Euler(0, -120, 0); 
            
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation * correction, Time.deltaTime * rotationSpeed);
        }
    }

}
