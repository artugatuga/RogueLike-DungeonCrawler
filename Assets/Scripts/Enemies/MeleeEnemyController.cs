using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    private GameObject Hitbox;
    private bool isAttacking = false;
    
    private float rotationSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        Hitbox = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);
        agent.isStopped = Vector3.Distance(transform.position, player.transform.position) < 3f;

        // Only look at player when stopped
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
        Hitbox.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Hitbox.SetActive(false);
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
