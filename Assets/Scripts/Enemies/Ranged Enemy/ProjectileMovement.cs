using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float lifetime = 5f;

    public Vector3 direction;
    
    private Transform target;
    private Rigidbody targetRigidbody;
    private float spawnTime;

    void Start()
    {
        StartCoroutine(Lifetime());
    }

    void FixedUpdate()
    {
        transform.position += direction.normalized * Time.fixedDeltaTime * speed;
    }
    
    private IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        IDamageable damageable = other.GetComponent<IDamageable>();

        damageable?.TakeDamage(damage, gameObject);
        Destroy(gameObject);
    }
}
