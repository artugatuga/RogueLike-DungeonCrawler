using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    private bool isAttacking = false;
    
    private Transform projectileSpawn;
    
    private float rotationSpeed = 5f;
    
    [SerializeField] private GameObject projectilePrefab;
    
    [SerializeField] private float maxDistance = 10f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        projectileSpawn = transform.Find("ProjectileSpawn");
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
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawn.position, transform.rotation);
        projectile.GetComponent<ProjectileMovement>().direction = transform.forward;
        yield return new WaitForSeconds(2f);
        isAttacking = false;
    }
    
    void LookAtPlayer()
    {
        if (player == null) return;

        Vector3 direction = player.transform.position - transform.position;
        
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}