using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    
    //Patrolling
    public Vector3 walkpoint;
    public bool walkPointSet;
    public float walkPointRange;
    public float moveBaseDelay;
    public float moveDelay;
    
    //Attacking
    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    
    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        moveDelay = moveBaseDelay;
    }

    private void Update()
    {
        if (moveDelay > 0)
        {
            moveDelay -= Time.deltaTime;
        }

        if (moveDelay < -1)
        {
            moveDelay = moveBaseDelay;
        }
        //check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        
        if(!playerInSightRange && !playerInAttackRange) Patroling();
        if(playerInSightRange && !playerInAttackRange) ChasePlayer();
        if(playerInSightRange && playerInAttackRange) AttackPlayer();
    }

 

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) agent.SetDestination(walkpoint);

        Vector3 distanceToWalkPoint = transform.position - walkpoint;
        
        //walkpoint reached
        if (moveDelay <= 0)
        {
            walkPointSet = false;
            moveDelay = moveBaseDelay;
        }

    }

    private void SearchWalkPoint()
    {
        //calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkpoint, -transform.up, 2f, whatIsGround))
        {
            
            Debug.Log("Raycast successful");
            walkPointSet = true;
        }
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        //make sure enemy doesnt move
        agent.SetDestination(transform.position);
        if (!alreadyAttacked)
        {
            //ATTACKS WILL GO HERE
            Debug.Log("ATTACK");
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    
}
