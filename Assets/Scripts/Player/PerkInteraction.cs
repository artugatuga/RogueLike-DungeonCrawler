using System;
using UnityEngine;

public class PerkInteraction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        
        IInteractable interactable = other.GetComponent<IInteractable>();
        
        if (interactable != null)
        {
            interactable.Interact(gameObject);
        }
    }
}
