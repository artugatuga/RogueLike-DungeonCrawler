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

        if (agent.isStopped && !isAttacking)
        {
            StartCoroutine(Attack());
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

}
