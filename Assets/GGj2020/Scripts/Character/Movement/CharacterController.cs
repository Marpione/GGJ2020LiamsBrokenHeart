using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;

public class CharacterController : MonoBehaviour
{
    private NavMeshAgent agent;
    public NavMeshAgent NavMeshAgent { get { return (agent == null) ? agent = GetComponent<NavMeshAgent>() : agent; } }


    private Animator animator;
    public Animator Animator { get { return (animator == null) ? animator = GetComponent<Animator>() : animator; } }

    LookAt lookAt;
    LookAt LookAt { get { return (lookAt == null) ? lookAt = GetComponent<LookAt>() : lookAt; } }

    Vector2 smoothDeltaPosition = Vector2.zero;
    Vector2 velocity = Vector2.zero;

    public InteractiveObject currentInteractiveObject;

    public bool canMove;

    void Start()
    {
        // Don’t update position automatically
        NavMeshAgent.updatePosition = false;
    }

    void Update()
    {
        if (!canMove)
            return;

        Vector3 worldDeltaPosition = agent.nextPosition - transform.position;

        // Map 'worldDeltaPosition' to local space
        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        // Low-pass filter the deltaMove
        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        // Update velocity if time advances
        if (Time.deltaTime > 1e-5f)
            velocity = smoothDeltaPosition / Time.deltaTime;

        bool shouldMove = velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;

        UpdateAnimator(shouldMove);

        LookAt.lookAtTargetPosition = agent.steeringTarget + transform.forward;

        // Pull character towards agent
        if (worldDeltaPosition.magnitude > agent.radius)
            transform.position = NavMeshAgent.nextPosition - 0.9f * worldDeltaPosition;





        if (!shouldMove)
        {
            if (currentInteractiveObject != null)
            {
                if (Vector3.Distance(transform.position, currentInteractiveObject.transform.position) < 5f)
                {
                    currentInteractiveObject.Interact();
                    currentInteractiveObject = null;
                }
            }
        }
    }

    void UpdateAnimator(bool shouldMove)
    {
        Animator.SetBool("Move", shouldMove);
        Animator.SetFloat("VelocityX", velocity.x);
        Animator.SetFloat("VelocityY", velocity.y);
    }

    void OnAnimatorMove()
    {
        // Update position to agent position
        Vector3 position = Animator.rootPosition;
        position.y = NavMeshAgent.nextPosition.y;
        transform.position = position;
    }
}
