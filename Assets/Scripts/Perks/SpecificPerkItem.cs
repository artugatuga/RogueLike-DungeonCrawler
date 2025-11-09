using System;
using UnityEngine;

public class SpecificPerkItem : MonoBehaviour, IInteractable
{
    private PerkType perkType;
    private ExtraPerkType extraPerkType;
    private OtherPerkType otherPerkType;
    
    private Inventory inventory;
    private Perk perkComponent;

    private void Start()
    {
        inventory = FindFirstObjectByType<Inventory>();
    }

    public void SetPerk(Perk perk)
    {
        perkComponent = perk;
    }
    
    void IInteractable.Interact(GameObject source)
    {
        if (inventory != null)
        {
            inventory.AddToInventory(perkComponent);
        }
    }
}
