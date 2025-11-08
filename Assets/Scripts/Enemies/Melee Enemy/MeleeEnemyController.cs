using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyController : MonoBehaviour
{
    private static readonly int Attack1 = Animator.StringToHash("Attack1");
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private NavMeshAgent agent;
    private GameObject player;
    private GameObject Hitbox;
    private bool isAttacking = false;
    
    private Animator animator;

    [SerializeField] private float attackCooldown = 3f;
    [SerializeField] private float attackDuration = 0.3f;
    
    private float rotationSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        Hitbox = transform.GetChild(1).gameObject;
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        agent.isStopped = Vector3.Distance(transform.position, player.transform.position) < 3f;

        if (!agent.isStopped)
        {
            animator.SetBool(IsMoving, true);
            agent.SetDestination(player.transform.position);
        }
        // Only look at player when stopped
        if (agent.isStopped)
        {
            animator.SetBool(IsMoving, false);
            LookAtPlayer();
        
            if (!isAttacking)
            {
                StartCoroutine(Attack());
            }
        }
    }

    public IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger(Attack1);
        Hitbox.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        Hitbox.SetActive(false);
        yield return new WaitForSeconds(attackCooldown);
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
