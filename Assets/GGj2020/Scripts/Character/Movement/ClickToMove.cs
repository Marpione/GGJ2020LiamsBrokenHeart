using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    RaycastHit hitInfo = new RaycastHit();
    NavMeshAgent agent;

    private NavMeshHit navHit;
    private bool blocked = false;

    CharacterController characterController;
    CharacterController CharacterController { get { return (characterController == null) ? characterController = GetComponent<CharacterController>() : characterController; } }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (!CharacterController.canMove)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                InteractiveObject interactiveObject = hitInfo.collider.GetComponent<InteractiveObject>();

                if (interactiveObject)
                    CharacterController.currentInteractiveObject = interactiveObject;
                else CharacterController.currentInteractiveObject = null;

                if (Vector3.Distance(transform.position, hitInfo.point) > 1f)
                {
                    GetComponent<Animator>().SetTrigger("Walk");
                    agent.destination = hitInfo.point;
                }
                    
            }
        }
    }
}
