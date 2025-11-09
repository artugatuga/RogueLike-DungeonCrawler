using System;
using UnityEngine;

public class PerkInteraction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        
        if (interactable != null)
        {
            interactable.Interact(gameObject);
            Destroy(other.gameObject);
        }
    }
}
