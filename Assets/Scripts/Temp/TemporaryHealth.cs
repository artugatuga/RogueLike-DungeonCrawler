using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TemporaryHealth : MonoBehaviour, IDamageable
{
    private static readonly int Death = Animator.StringToHash("Death");

    [SerializeField]
    private float Health = 100f;
    public UnityEvent OnTakeDamage;
    
    private NavMeshAgent agent;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        OnTakeDamage.AddListener(CheckIfDead);
        agent = GetComponent<NavMeshAgent>();
    }
    public void TakeDamage(float damage, GameObject source)
    {
        Health -= damage;
        OnTakeDamage.Invoke();
        Debug.Log(gameObject.name + " taking damage");
        Debug.LogWarning("Current Health: " + Health);
        // if (gameObject.CompareTag("Enemy")) GetComponent<Rigidbody>().linearVelocity = transform.forward * -100f;
    }

    void CheckIfDead()
    {
        if (Health <= 0f)
        {
            agent.isStopped = true;
            animator.SetTrigger(Death);
            if (this.gameObject.CompareTag("Player"))
            {
                
            }
            if (this.gameObject.CompareTag("Enemy"))
            {
                StartCoroutine(DestroyGameObject());
            }
        }
    }

    IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}
