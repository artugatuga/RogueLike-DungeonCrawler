using System;
using UnityEngine;

public class PerkInteraction : MonoBehaviour
{
    [SerializeField] private Collider mainCollider;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Perk")) return;
        
        IInteractable interactable = other.GetComponent<IInteractable>();
        
        if (interactable != null)
        {
            interactable.Interact(gameObject);
            Destroy(other.gameObject);
        }
    }
}
