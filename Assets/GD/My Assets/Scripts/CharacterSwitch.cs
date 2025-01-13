using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class BoatSwitch : MonoBehaviour
{
    public GameObject Human;
    public GameObject Boat;
    public CinemachineVirtualCamera playerCamera;

    private bool onPort = false; // To check for player in trigger area
    private NavMeshAgent currentAgent; // This is the current navmesh agent (human or boat)

    void Update()
    {
        if (onPort && Input.GetKeyDown(KeyCode.Q))
        {
            SwitchCharacter();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPort = true;
            Debug.Log("Press Q to switch to boat");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPort = false;
        }
    }

    // This method switches between the human and the boat - activates and deactivates the game objects
    void SwitchCharacter()
    {
        if (Human.activeSelf)
        {
            Human.SetActive(false);
            Boat.SetActive(true);

            // Update the Cinemachine camera to follow the boat instead of human 
            playerCamera.Follow = Boat.transform;
            playerCamera.LookAt = Boat.transform;

            // This enables navmesh for the boat and disable it for the human
            NavMeshAgent boatAgent = Boat.GetComponent<NavMeshAgent>();
            if (boatAgent != null)
            {
                boatAgent.enabled = true;
                currentAgent = boatAgent;
            }

            // Disable navmesh for the human
            NavMeshAgent humanAgent = Human.GetComponent<NavMeshAgent>();
            if (humanAgent != null)
            {
                humanAgent.enabled = false;
            }
        }

        // If the boat is active, switch to the human when E is pressed in the trigger area (oposite)
        else
        {
            Boat.SetActive(false);
            Human.SetActive(true);

            playerCamera.Follow = Human.transform;
            playerCamera.LookAt = null; // Reverts the look at to default which is null for a static top down view

            NavMeshAgent humanAgent = Human.GetComponent<NavMeshAgent>();
            if (humanAgent != null)
            {
                humanAgent.enabled = true;
                currentAgent = humanAgent;
            }

            NavMeshAgent boatAgent = Boat.GetComponent<NavMeshAgent>();
            if (boatAgent != null)
            {
                boatAgent.enabled = false;
            }
        }
    }
}
