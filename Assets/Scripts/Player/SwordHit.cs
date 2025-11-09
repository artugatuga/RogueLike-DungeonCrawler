using System;
using UnityEngine;

public class SwordHit : MonoBehaviour
{
    [SerializeField] private PlayerManager PlayerManager;
    [SerializeField] private ParticleSystem myParticleSystem;

    void OnEnable()
    {
        myParticleSystem.Play();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.gameObject.GetComponentInParent<IDamageable>();
        
        damageable?.TakeDamage(PlayerManager.damage, this.gameObject);
    }
}
