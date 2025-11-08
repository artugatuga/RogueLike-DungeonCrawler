using UnityEngine;

public class TemporaryHealth : MonoBehaviour, IDamageable
{
    private float Health = 100f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float damage, GameObject source)
    {
        Health -= damage;
        Debug.Log(gameObject.name + " taking damage");
        Debug.LogWarning("Current Health: " + Health);
    }
}
