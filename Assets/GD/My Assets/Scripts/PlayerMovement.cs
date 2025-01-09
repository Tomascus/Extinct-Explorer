using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera; // Player camera to get the mouse position when clicking 
    private NavMeshAgent navMeshAgent; 

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); // Get the player nav mesh agent for movement
        // Add check if there is no camera ????
    }

    void Update()
    {
        // Check if the player clicked left mouse button
        if (Input.GetMouseButtonDown(0)) 
        {
            movePlayer();
        }
    }

    void movePlayer()
    {
        // We start by getting a ray that goes from the camera through the mouse position and then we check if it hits something
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // If the ray hits something, we set the destination of the NavMeshAgent to the clicked point 
        if (Physics.Raycast(ray, out hit))
        {
            navMeshAgent.SetDestination(hit.point);
        }
    }
}