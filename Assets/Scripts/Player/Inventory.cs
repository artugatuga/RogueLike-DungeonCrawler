using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject slotUIExample;
    [SerializeField] private GameObject dropedItemExample;
    [SerializeField] private GameObject inventoryBackgroundUI;
    [SerializeField] private GameObject inventoryItemExampleUI;
    [SerializeField] private List<GameObject> inventoryItemsUI;
    [SerializeField] private PerksManagerUI perksManagerUI;
    
    private Dictionary<string, List<Perk>> perkStacks = new Dictionary<string, List<Perk>>();
    private bool isInventoryOn = false;

    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            if (!isInventoryOn) ShowInventoryUI(); else HideInventoryUI();
        }
    }
    
    public void AddToInventory(Perk perk)
    {
        string perkKey = GetPerkKey(perk);
        
        if (!perkStacks.ContainsKey(perkKey))
        {
            perkStacks[perkKey] = new List<Perk>();
        }
        
        perkStacks[perkKey].Add(perk);
        
        // Apply effect with new stack count
        perk.ApplyPerkEffect(perkStacks[perkKey].Count);
        
        Debug.Log($"Added {perkKey}. Total stacks: {perkStacks[perkKey].Count}");
    }

    void ShowInventoryUI()
    {
        isInventoryOn = true;
        inventoryBackgroundUI.SetActive(true);
        int i = 0;
        
        foreach (var perkStack in perkStacks)
        {
            Vector3 newPosition = inventoryBackgroundUI.transform.position;
            RectTransform rectTransform = inventoryBackgroundUI.GetComponent<RectTransform>();
            GameObject newItem = Instantiate(inventoryItemExampleUI, inventoryBackgroundUI.transform);
            
            string displayName = perkStack.Key;
            if (perkStack.Value.Count > 1)
            {
                displayName += $" x{perkStack.Value.Count}";
            }
            
            newItem.name = displayName;

            float totalWidth = 700f * i;
            float startX = rectTransform.position.x - totalWidth / 2f + 100f;

            newItem.transform.position = new Vector3(startX + 200f * i, newPosition.y, newPosition.z);
            newItem.GetComponentInChildren<TextMeshProUGUI>().text = displayName;
            
            string perkKey = perkStack.Key;
            newItem.GetComponentInChildren<Button>().onClick.AddListener(() => CallDropItem(perkKey));
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
        
        inventoryItemsUI.Clear();
        inventoryBackgroundUI.SetActive(false);
        isInventoryOn = false;
    }
    
    public void DropAndRemoveFromInventory(string perkKey)
    {
        if (perkStacks.ContainsKey(perkKey) && perkStacks[perkKey].Count > 0)
        {
            // Remove the last added perk (most recent)
            Perk perkToRemove = perkStacks[perkKey][perkStacks[perkKey].Count - 1];
            perkStacks[perkKey].RemoveAt(perkStacks[perkKey].Count - 1);
            
            // FIXED: Call RemovePerkEffect on the perk being removed
            int newStackCount = perkStacks.ContainsKey(perkKey) ? perkStacks[perkKey].Count : 0;
            perkToRemove.RemovePerkEffect(newStackCount);
            
            // Remove empty stacks
            if (perkStacks[perkKey].Count == 0)
            {
                perkStacks.Remove(perkKey);
            }
            else
            {
                // Reapply effects with new stack count for remaining perks
                perkStacks[perkKey][0].ApplyPerkEffect(perkStacks[perkKey].Count);
            }
            
            // Spawn the dropped perk in world
            Vector3 newPosition = transform.position + transform.forward * 5;
            GameObject item = Instantiate(dropedItemExample, newPosition, Quaternion.identity, transform.parent);
            SpecificPerkItem itemComponent = item.GetComponent<SpecificPerkItem>();
            
            if (itemComponent != null)
            {
                itemComponent.SetPerk(perkToRemove);
            }
            
            int remainingStacks = perkStacks.ContainsKey(perkKey) ? perkStacks[perkKey].Count : 0;
            Debug.Log($"Dropped {perkKey}. Remaining stacks: {remainingStacks}");
        }
    }
    
    public void CallDropItem(string perkKey)
    {
        HideInventoryUI();
        DropAndRemoveFromInventory(perkKey);
    }
    
    private string GetPerkKey(Perk perk)
    {
        if (perk.perkType == PerkType.Extra)
        {
            return perk.extraPerkType.ToString();
        }
        else
        {
            return perk.otherPerkType.ToString();
        }
    }
}