using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public bool isStopped = false;
    PlayerMovements player;
    NavMeshAgent agent;
    public int comp = 0;
    public Transform[] spotsToReach;
    void Start()
    {
        player = FindObjectOfType<PlayerMovements>();

        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        agent.isStopped = isStopped;
        Move();
    }

    public void Move()
    {
        if (spotsToReach.Length == 0)
        {
            // La liste est vide, il n'y a rien à faire
            return;
        }

        if (agent.remainingDistance == 0)
        {
            comp = (comp + 1) % spotsToReach.Length; // j'incrémentez le compteur et modifie sa valeur pour qu'elle reste dans les limites de la liste
            agent.SetDestination(spotsToReach[comp].position); // je définis la nouvelle destination de l'agent

            if (spotsToReach[comp].position.x <= transform.position.x) //Si le spot à atteindre est à gauche alors on set la rotation a 180 sinon à 0
            {
                transform.localEulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
            }
        }
    }
}
