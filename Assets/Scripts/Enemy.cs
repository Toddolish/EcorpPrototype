using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //declaration
    public enum State
    {
        Patrol, Seek, feeding
    }
    [Header("SEEK RADIUS")]

    [Header("When Crouched")]
    public float crouchRadius;

    [Header("When Sprinting")]
    public float sprintingRadius;

    [Header("When Sprinting with Light ON")]
    public float sprintingAndLightRadius;

    [Header("When Crouched with light is ON")]    
    public float lightAndCrouchRadius;

    Animator anim;
    public State currentState = State.Patrol;
    public Transform Player;
    public Transform waypointParent;
    public Transform feedingPoint;
    public float seekRadius = 5f;

    //create a collection of transforms.
    public Transform[] waypoints;
    private int currentIndex = 0;
    public float range;
    public float moveSpeed;

    PlayerMovement playerScript;
    PlayerSelect playerSelectScript;


    //ctrl + k + d
    //camelCasing - variables
    //PascalCasing - function and class names

    public NavMeshAgent agent;
    //public Transform target;
    private void Start()
    {
        //getting children of waypoint parent.
        waypoints = waypointParent.GetComponentsInChildren<Transform>();
        playerScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerSelectScript = GameObject.Find("Player").GetComponent<PlayerSelect>();
        anim = GetComponent<Animator>();
    }

    //////////////////update
    void Update()
    {
        EnemyDetectionRadius();

        agent.speed = moveSpeed;
        //switch current state
        //if we are in patrol
        //call patrol()
        //if me are in seek
        //call seek()
        switch (currentState)
        {
            case State.Patrol:
                //patrol state
                Patrol();
                break;

            case State.Seek:
                //seek state
                Seek();
                break;

            case State.feeding:
                //seek state
                Feed();
                break;

            default:
                break;
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, seekRadius);
    }
    void Patrol()
    {
        anim.SetBool("eating", false);
        //move towards waypoints
        moveSpeed = 1;
        Transform point = waypoints[currentIndex];
        float distance = Vector3.Distance(agent.transform.position, point.position);

        if (distance < range)
        {
            currentIndex++;
        }
        if (currentIndex >= waypoints.Length)
        {
            currentIndex = 0;
        }
        float disToTarget = Vector3.Distance(transform.position, Player.position);
        if(disToTarget < seekRadius)
        {
            currentState = State.Seek;
        }
        agent.SetDestination(point.position);
        // transform.position = Vector3.MoveTowards(transform.position, point.position, 0.1f);
        
    }
    void Seek()
    {
        anim.SetBool("eating", true);
        moveSpeed = 3.8f;
        //move towards player
        float distance = Vector3.Distance(agent.transform.position, Player.position);

        if (distance < range)
        {
            currentIndex++;
        }
        if (currentIndex >= waypoints.Length)
        {
            currentIndex = 0;
        }
        float disToTarget = Vector3.Distance(transform.position, Player.position);
        if (disToTarget > seekRadius)
        {
            currentState = State.Patrol;
        }
        agent.SetDestination(Player.position);
    }
    void EnemyDetectionRadius()
    {

        ///////////////////////////////////////////////////HIDING
        if (playerScript.hiding && !playerSelectScript.lightIsOn)
        {
            seekRadius = 1;
        }
        if (playerScript.hiding && playerSelectScript.lightIsOn)
        {
            seekRadius = 3;
        }

        ///////////////////////////////////////////////////CROUCHED
        if (playerScript.crouched && !playerSelectScript.lightIsOn)
        {
            seekRadius = 4;
        }
        if (playerScript.crouched && playerSelectScript.lightIsOn)
        {
            seekRadius = 6;
        }

        ////////////////////////////////////////////////////WALKING
        if (!playerScript.crouched && !playerScript.sprinting && !playerSelectScript.lightIsOn)
        {
            seekRadius = 7;
        }
        if (!playerScript.crouched && !playerScript.sprinting && playerSelectScript.lightIsOn)
        {
            seekRadius = 8;
        }

        //////////////////////////////////////////////////SPRINTING
        if (playerScript.sprinting)
        {
            seekRadius = 10;
        }
        if (playerScript.sprinting && playerSelectScript.lightIsOn)
        {
            seekRadius = 12;
        }
    }
    void Feed()
    {
        anim.SetBool("eating", false);
        moveSpeed = 2;
        Transform point = waypoints[currentIndex];
        float distance = Vector3.Distance(agent.transform.position, point.position);
        agent.SetDestination(feedingPoint.position);
    }
}
