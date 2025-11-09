using System;
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

    private float kockBackPos;
    private bool playKock;

    private bool dead = false;
    
    [SerializeField]private AudioSource audioSource;
    [SerializeField]private AudioClip DeathSound;
    
    
    
    [SerializeField] private GameObject deathScreen;
    
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
        if (gameObject.CompareTag("Enemy"))
        {
            playKock = true;
        }
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

    void Update()
    {
        if (playKock)
        {
            EnemyKnockBack();
        }
    }
    
    void EnemyKnockBack()
    {
        float previousPos = kockBackPos;
        
        kockBackPos += Time.deltaTime * 10;
        
        float delta = kockBackPos - previousPos;
        agent.enabled = false;
        if (kockBackPos >= 5)
        {
            playKock = false;
            kockBackPos = 0;
            agent.Warp(transform.position);
            if(!dead) agent.enabled = true;
            return;
        }
        
        
        transform.position -= transform.forward * delta;
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
            if (this.gameObject.CompareTag("Player") && !dead)
            {
                audioSource.clip = DeathSound;
                audioSource.Play();
                dead = true;
                animator.SetTrigger(Death);
                deathScreen.SetActive(true);
                GetComponent<PlayerMovement>().dead = true;
                GetComponent<PlayerAttack>().dead = true;
            }
            if (this.gameObject.CompareTag("Enemy") && !dead)
            {
                dead = true;
                agent.isStopped = true;
                agent.enabled = false;
                agent.enabled = false;
                StartCoroutine(DestroyGameObject());
            }
        }
    }

    IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(1f);
        itemSpawner.SpawnItem();
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (gameObject.CompareTag("Enemy"))
        {
            playKock = false;
            kockBackPos = 0;
            agent.Warp(transform.position);
            if(!dead) agent.enabled = true;
            return;
        }
    }
}
