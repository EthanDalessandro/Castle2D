using System.Collections.Generic;
using UnityEngine;

public class ALLMOBCONTROLLER : MonoBehaviour
{
    public List<EnemyAI> enemyAgents;
    public PlayerMovements player;

    void Start()
    {
        player = FindObjectOfType<PlayerMovements>();
        enemyAgents = new List<EnemyAI>(FindObjectsOfType<EnemyAI>());
    }

    public void StopAllEnemy()
    {
        for (int i = 0; i < enemyAgents.Count; i++)
        {
            enemyAgents[i].isStopped = true;
        }
    }

    public void ResumeAllEnemyMovement()
    {
        for (int i = 0; i < enemyAgents.Count; i++)
        {
            enemyAgents[i].isStopped = false;
        }
    }
}
