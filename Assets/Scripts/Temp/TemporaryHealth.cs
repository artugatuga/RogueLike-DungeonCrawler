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

    private ItemSpawner itemSpawner;
    
    private NavMeshAgent agent;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private PlayerManager playerManager;
    
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        OnTakeDamage.AddListener(CheckIfDead);
        playerManager = FindFirstObjectByType<PlayerManager>();
        if (gameObject.CompareTag("Enemy"))
        {
            itemSpawner = gameObject.GetComponent<ItemSpawner>();
            agent = GetComponent<NavMeshAgent>();
        }
    }
    
    public void TakeDamage(float damage, GameObject source)
    {
        Health -= damage;
        
        float maxHealth = 0;
        if (playerManager)
        {
            maxHealth = playerManager.maxHealth;
        }
        
        Health = Mathf.Clamp(Health, 0, maxHealth);
        
        OnTakeDamage.Invoke();
        
        Debug.Log(gameObject.name + " taking damage");
        Debug.LogWarning("Current Health: " + Health);
        // if (gameObject.CompareTag("Enemy")) GetComponent<Rigidbody>().linearVelocity = transform.forward * -100f;
    }
    public void AddHealth(float addHealth)
    {
        Health += addHealth;
        
        float maxHealth = 0;
        if (playerManager)
        {
            maxHealth = playerManager.maxHealth;
        }
        
        Health = Mathf.Clamp(Health, 0, maxHealth);
    }
    
    public void AddToMaxHealth(float addHealth)
    {
        playerManager.maxHealth += addHealth;
    }
    
    public void TakeFromMaxHealth(float removeHealth)
    {
        playerManager.maxHealth -= removeHealth;
    }

    void CheckIfDead()
    {
        if (Health <= 0f)
        {
            animator.SetTrigger(Death);
            if (this.gameObject.CompareTag("Player"))
            {
                
            }
            if (this.gameObject.CompareTag("Enemy"))
            {
                agent.isStopped = true;
                StartCoroutine(DestroyGameObject());
            }
        }
    }

    IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(1.5f);
        itemSpawner.SpawnItem();
        Destroy(this.gameObject);
    }
}
