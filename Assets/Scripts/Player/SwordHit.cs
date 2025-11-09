using System;
using UnityEngine;

public class SwordHit : MonoBehaviour
{
    [SerializeField] PlayerManager PlayerManager;
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.gameObject.GetComponentInParent<IDamageable>();
        
        damageable?.TakeDamage(PlayerManager.damage, this.gameObject);
    }
}
