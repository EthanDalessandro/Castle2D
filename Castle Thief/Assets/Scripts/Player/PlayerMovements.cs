using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
public class PlayerMovements : MonoBehaviour
{
    public float timeBeforeInvisiblity = 0f;
    public float cooldownForNextPathSpot = 0.2f;
    private float cooldownForNextPathSpotContainer;
    float cooldownPrefab;
    public bool isMoving;
    public bool isSettingPath;
    public bool isSaw = false;
    SpriteRenderer playerSprite;
    float timeBeforeInvisiblityCoolDown;
    NavMeshAgent agent;
    private Vector2 lastClickedPosition;
    public GameObject pathSpotPrefab;
    public int limitPathCount = 15;
    public List<GameObject> pathPlayerList;
    ALLMOBCONTROLLER allMob;
    void Start()
    {
        cooldownForNextPathSpotContainer = cooldownForNextPathSpot;
        playerSprite = GetComponent<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
        allMob = FindObjectOfType<ALLMOBCONTROLLER>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isSaw = !isSaw;
        }
        Movement();
        Rotation();
        Invisibility();
    }

    void Movement()
    {
        if (isSaw)
        {
            agent.isStopped = true;
            return;
        }
        else
        {
            agent.isStopped = false;
        }
        if (Input.GetKey(KeyCode.Mouse0)) //tant que je reste appuyer sur la clique gauche
        {

            lastClickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (pathPlayerList.Count == 0)
            {
                cooldownForNextPathSpot = 0f;
            }
            else
            {
                cooldownForNextPathSpot = cooldownForNextPathSpotContainer;
            }

            cooldownPrefab += Time.deltaTime;
            if ((cooldownPrefab >= cooldownForNextPathSpot) && (isMoving == false) && (pathPlayerList.Count + 1 <= limitPathCount))
            {
                isSettingPath = true;
                allMob.StopAllEnemy();
                GameObject instantiatedPrefab = Instantiate(pathSpotPrefab, lastClickedPosition, Quaternion.identity);
                pathPlayerList.Add(instantiatedPrefab);
                cooldownPrefab = 0;
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0)) // Une fois que je relâche le clique gauche
        {
            isSettingPath = false;
            allMob.ResumeAllEnemyMovement();

            if (pathPlayerList.Count == 0)
            {
                return;
            }
        }

        if (isSettingPath == false && pathPlayerList.Count > 0)
        {
            if (agent.remainingDistance <= 0.3f)
            {
                Destroy(pathPlayerList[0]);
                pathPlayerList.RemoveAt(0);

                if (pathPlayerList.Count == 0 || isSaw)
                {
                    return;
                }

                agent.SetDestination(pathPlayerList[0].transform.position);
            }
        }

        if (agent.remainingDistance > 0.1f) //on verifie si le player bouge ou non
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }
    void Invisibility()
    {
        if (isMoving == false)
        {
            timeBeforeInvisiblityCoolDown += Time.deltaTime;

            if (timeBeforeInvisiblityCoolDown >= timeBeforeInvisiblity)
            {
                playerSprite.color = new Color(1f, 1f, 1f, 0.5f);
            }
        }
        else
        {
            timeBeforeInvisiblityCoolDown = 0f;
            playerSprite.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    void Rotation()
    {
        if (pathPlayerList.Count > 0)
        {
            if (pathPlayerList[0].transform.position.x <= transform.position.x) //Si le spot à atteindre est à gauche alors on set la rotation a 180 sinon à 0
            {
                transform.localEulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
            }
        }
        else
        {
            return;
        }
    }
}
