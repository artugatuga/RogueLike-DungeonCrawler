using UnityEngine;

public class PerkItem : MonoBehaviour, IInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private PerksManager perksManager;
    
    void Start()
    {
        perksManager = FindFirstObjectByType<PerksManager>();
    }

    void IInteractable.Interact(GameObject source)
    {
        if (perksManager != null)
        {
            perksManager.RandomizePerkSelection();
            Destroy(gameObject);
        }
    }
}
