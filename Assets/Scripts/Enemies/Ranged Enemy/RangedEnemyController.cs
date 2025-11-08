using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    private bool isAttacking = false;
    
    private float rotationSpeed = 5f;
    
    [SerializeField] private GameObject projectilePrefab;
    
    [SerializeField] private float maxDistance = 10f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        agent.destination = player.transform.position;

        agent.isStopped = (Vector3.Distance(player.transform.position, agent.transform.position) <= maxDistance);

        if (agent.isStopped)
        {
            LookAtPlayer();

            if (!isAttacking)
            {
                StartCoroutine(Attack());
            }
        }
    }
    
    IEnumerator Attack()
    {
        isAttacking = true;
        Instantiate(projectilePrefab, player.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        isAttacking = false;
    }
    
    void LookAtPlayer()
    {
        if (player == null) return;

        // Get direction to player (ignore Y-axis for typical ground enemies)
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0; // This keeps the enemy upright

        // Only rotate if there's a meaningful direction
        if (direction != Vector3.zero)
        {
            // Create target rotation
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            
            // Smoothly rotate towards target
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}