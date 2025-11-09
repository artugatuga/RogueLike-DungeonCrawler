using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SwordHit : MonoBehaviour
{
    [SerializeField] private PlayerManager PlayerManager;
    [SerializeField] private ParticleSystem myParticleSystem;
    [SerializeField] private AudioSource myAudioSource;
    [SerializeField] private AudioClip hitSound;
    private GameObject enemyHit;

    void OnEnable()
    {
        myParticleSystem.Play();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.gameObject.GetComponentInParent<IDamageable>();
        
        if (enemyHit == other.gameObject || damageable == null) return;
        
        enemyHit = other.gameObject;
        
        damageable?.TakeDamage(PlayerManager.damage, this.gameObject);
        
        myAudioSource.clip = hitSound;
        myAudioSource.pitch = Random.Range(0.8f, 1.2f);
        myAudioSource.Play();
    }
}
