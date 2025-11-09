using System;
using UnityEngine;

public class PerkInteraction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Perk")) return;
        
        Debug.Log(other.gameObject.name);
        
        IInteractable interactable = other.GetComponent<IInteractable>();
        
        Debug.Log(interactable);
        
        if (interactable != null)
        {
            Debug.Log("CalledInteract");
            interactable.Interact(gameObject);
        }
    }
}
