using UnityEngine;

public class SpecificPerkItem : Singleton<SpecificPerkItem>, IInteractable
{
    private Perk perkData;
    private Inventory inventory;

    private void Start()
    {
        inventory = FindFirstObjectByType<Inventory>();
    }

    public void SetPerk(Perk perk)
    {
        perkData = perk;
    }
    
    void IInteractable.Interact(GameObject source)
    {
        if (inventory != null && perkData != null)
        {
            inventory.AddToInventory(perkData);
            Destroy(gameObject);
        }
    }
}