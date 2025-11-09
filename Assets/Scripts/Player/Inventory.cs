using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject slotUIExample;
    [SerializeField] private GameObject dropedItemExample;
    [SerializeField] private GameObject inventoryBackgroundUI;
    [SerializeField] private GameObject inventoryItemExampleUI;
    [SerializeField] private List<GameObject> inventoryItemsUI;
    [SerializeField] private List<Perk> currentIventoryPerks;
    [SerializeField] private PerksManagerUI perksManagerUI;
    
    private bool isInventoryOn = false;

    private void Start()
    {
        currentIventoryPerks = new List<Perk>();
    }

    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            if (!isInventoryOn) ShowInventoryUI(); else HideInventoryUI();
        }
    }
    
    public void AddToInventory(Perk perk)
    {
        currentIventoryPerks.Add(perk);
        perk.CallPerkFunction();
    }

    void ShowInventoryUI()
    {
        isInventoryOn = true;
        inventoryBackgroundUI.SetActive(true);
        int i = 0;
        
        foreach (Perk item in currentIventoryPerks)
        {
            Vector3 newPosition = inventoryBackgroundUI.transform.position;
            RectTransform rectTransform = inventoryBackgroundUI.GetComponent<RectTransform>();
            GameObject newItem = Instantiate(inventoryItemExampleUI, inventoryBackgroundUI.transform);
            newItem.name = item.perkType.ToString() + item.extraPerkType.ToString() + item.otherPerkType.ToString();

            float totalWidth = 700f * i; // Total width needed for all items
            float startX = rectTransform.position.x - totalWidth / 2f + 100f; // Center the group and adjust for first item position

            newItem.transform.position = new Vector3(startX + 200f * i, newPosition.y, newPosition.z);
            newItem.GetComponentInChildren<TextMeshProUGUI>().text = newItem.name;
            
            int index = i;
            newItem.GetComponentInChildren<Button>().onClick.AddListener(() => CallDropItem(index));
            newItem.SetActive(true);
            inventoryItemsUI.Add(newItem);
            i++;
        }
    }
    
    void HideInventoryUI()
    {
        foreach (GameObject item in inventoryItemsUI)
        {
            Destroy(item);
        }
        
        inventoryItemsUI.RemoveRange(0, inventoryItemsUI.Count);
        
        inventoryBackgroundUI.SetActive(false);
        isInventoryOn = false;
    }
    
    public void DropAndRemoveFromInventory(Perk perk)
    {
        Vector3 newPosition = transform.position + transform.forward * 5;
        
        GameObject item = Instantiate(dropedItemExample, transform.parent);
        SpecificPerkItem itemComponent = item.GetComponent<SpecificPerkItem>();
        item.transform.position = newPosition;

        foreach (Perk currentPerk in currentIventoryPerks)
        {
            if (currentPerk == perk)
            {
                perk.CallRemovePerkFunction();
                currentIventoryPerks.Remove(perk);
                break;
            }
        }
        
        if (itemComponent != null)
        {
            itemComponent.SetPerk(perk);
        }
        
        currentIventoryPerks.Remove(perk);
    }
    
    public void CallDropItem(int perkIndex)
    {
        HideInventoryUI();
        DropAndRemoveFromInventory(currentIventoryPerks[perkIndex]);
    }
}
