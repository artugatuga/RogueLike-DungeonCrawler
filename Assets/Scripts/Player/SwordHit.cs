using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SwordHit : MonoBehaviour
{
    [SerializeField] private PlayerManager PlayerManager;
    [SerializeField] private ParticleSystem myParticleSystem;
    [SerializeField] private AudioSource myAudioSource;
    [SerializeField] private AudioClip hitSound;
    private float crit = 0;
    private float size = 1;
    private float lifeSteal = 0;
    private GameObject enemyHit;
    [SerializeField] private GameObject player;

    void OnEnable()
    {
        myParticleSystem.Play();
        
        transform.localScale = new Vector3(size, size, size);
        myParticleSystem.startSize = size;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.gameObject.GetComponentInParent<IDamageable>();
        
        if (enemyHit == other.gameObject || damageable == null) return;
        
        enemyHit = other.gameObject;

        float dealtDamage = PlayerManager.damage * Crit();
        
        damageable?.TakeDamage(dealtDamage, this.gameObject);

        lifeSteal = PlayerManager.lifeSteal;
        
        player.GetComponent<TemporaryHealth>().AddHealth(dealtDamage * lifeSteal/100);
        
        myAudioSource.clip = hitSound;
        myAudioSource.pitch = Random.Range(0.8f, 1.2f);
        myAudioSource.Play();
    }

    private int Crit()
    {
        crit = PlayerManager.critDamage;
        if (Random.Range(0f, 100f) < crit) return 2;
        
        return 1;
    }
}
