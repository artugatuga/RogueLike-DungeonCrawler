using System;
using UnityEngine;

public class SwordHit : MonoBehaviour
{
    [SerializeField] PlayerManager PlayerManager;
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            other.gameObject.GetComponent<IDamageable>().TakeDamage(PlayerManager.damage, this.gameObject);
        }
    }
}
