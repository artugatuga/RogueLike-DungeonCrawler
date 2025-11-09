using UnityEngine;

public class SpecificPerkItem : MonoBehaviour, IInteractable
{
    private Perk perkData;
    private Inventory inventory;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

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
        inventory = FindFirstObjectByType<Inventory>();
        Debug.Log("CalledInteract");
        Debug.Log(perkData);
        Debug.Log(inventory);
        if (inventory != null && perkData != null)
        {
            Debug.Log("Has Everything");
            inventory.AddToInventory(perkData);
            Destroy(gameObject);
        }
    }
}