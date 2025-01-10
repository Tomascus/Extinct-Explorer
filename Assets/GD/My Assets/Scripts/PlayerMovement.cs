using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera playerCamera; // Player camera to get the mouse position when clicking 
    private NavMeshAgent navMeshAgent;

    [Header("Animation Settings")]
    private Animator animator;
    private float lastClickTime = 0f;
    [SerializeField] private float doubleClickTime = 0.25f; // Time buffer for double click - gives small marging of wait time to double click for sprint
    private bool isSprinting = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); 
        animator = GetComponent<Animator>(); 

        // Defaults to main camera if player camera is not set
        if (playerCamera == null)
        {
            playerCamera = Camera.main; 
        }
    }

    void Update()
    {
        HandleMovement();
        HandleAnimation();
    }

    void HandleMovement()
    {
        // Checks if the player left clicks
        if (Input.GetMouseButtonDown(0))
        {
            // Here it calculates time since last click
            float timeSinceClick = Time.time - lastClickTime;
            lastClickTime = Time.time;

            if (timeSinceClick <= doubleClickTime)
            {
                isSprinting = true;
                navMeshAgent.speed = 7f; 
            }
            else
            {
                isSprinting = false;
                navMeshAgent.speed = 3.5f;
            }

            MovePlayer();
        }
    }

    void MovePlayer()
    {
        // We start by getting a ray that goes from the camera through the mouse position and then we check if it hits something
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // If the ray hits something, we set the destination of the NavMeshAgent to the clicked point 
            navMeshAgent.SetDestination(hit.point);
        }
    }

    void HandleAnimation()
    {
        // This checks if there are no animations for the player
        if (animator == null) return;

        // Check if the player has reached the destination - if so stop the walking and sprinting animations
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            // Stops animations when player reaches click point
            animator.SetBool("Walk", false);
            animator.SetBool("Sprint", false);
        }
        else
        {
            // Sets animations for walking and sprinting while moving towards click point
            animator.SetBool("Walk", !isSprinting);
            animator.SetBool("Sprint", isSprinting);
        }
    }
}


