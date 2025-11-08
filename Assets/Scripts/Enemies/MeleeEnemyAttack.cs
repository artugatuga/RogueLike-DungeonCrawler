using System;
using UnityEngine;

public class MeleeEnemyAttack : MonoBehaviour
{
    [SerializeField]
    float damage;

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable == null) return;
        
        if (!other.CompareTag("Player")) return;
        
        damageable.TakeDamage(damage, gameObject);
    }
}
