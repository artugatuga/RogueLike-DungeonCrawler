using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyController : MonoBehaviour
{
    private static readonly int Shoot = Animator.StringToHash("Shoot");
    private static readonly int isWalking = Animator.StringToHash("isWalking");
    private NavMeshAgent agent;
    private GameObject player;
    private bool isAttacking = false;
    
    private Transform projectileSpawn;
    
    private float rotationSpeed = 5f;
    
    private Animator animator;
    
    [SerializeField] private GameObject projectilePrefab;
    
    [SerializeField] private float maxDistance = 10f;
    
    [SerializeField] private float damage = 10f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        projectileSpawn = transform.Find("ProjectileSpawn");
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled == false) return;
        
        agent.isStopped = (Vector3.Distance(player.transform.position, agent.transform.position) <= maxDistance);

        if (!agent.isStopped)
        {
            animator.SetBool(isWalking, true);
            agent.destination = player.transform.position;
        }

        if (agent.isStopped)
        {
            animator.SetBool(isWalking, false);
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
        Vector3 spawnEuler = projectileSpawn.rotation.eulerAngles;
        Quaternion rotation = Quaternion.Euler(90f, spawnEuler.y, spawnEuler.z);
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawn.position, rotation);
        ProjectileMovement projectileMovement = projectile.GetComponent<ProjectileMovement>();
        projectileMovement.damage = damage;
        projectileMovement.direction = transform.forward;
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