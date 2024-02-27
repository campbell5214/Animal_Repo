using UnityEngine;
using UnityEngine.AI;

public class ANIMAL_CHASE : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player if not manually assigned
        }
    }

    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position); // Set the player's position as the destination
        }
    }
}
