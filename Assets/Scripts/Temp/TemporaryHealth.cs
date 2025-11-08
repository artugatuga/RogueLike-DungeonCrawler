using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TemporaryHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float Health = 100f;
    public UnityEvent OnTakeDamage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnTakeDamage.AddListener(CheckIfDead);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float damage, GameObject source)
    {
        Health -= damage;
        OnTakeDamage.Invoke();
        Debug.Log(gameObject.name + " taking damage");
        Debug.LogWarning("Current Health: " + Health);
    }

    void CheckIfDead()
    {
        if (Health <= 0f)
        {
            if (this.gameObject.CompareTag("Player"))
            {
                
            }
            if (this.gameObject.CompareTag("Enemy"))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
