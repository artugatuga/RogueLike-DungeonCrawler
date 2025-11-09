using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject slotUIExample;
    [SerializeField] private GameObject dropedItemExample;
    [SerializeField] private GameObject inventoryBackgroundUI;
    [SerializeField] private GameObject inventoryItemExampleUI;
    [SerializeField] private List<GameObject> inventoryItemsUI;
    [SerializeField] private List<GameObject> sideBarItemsUI;
    [SerializeField] private PerksManagerUI perksManagerUI;
    [SerializeField] private GameObject sideBarParent;
    
    private Dictionary<string, List<Perk>> perkStacksUI = new Dictionary<string, List<Perk>>();
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
        
        Debug.Log("Inventory");
        
        if (!perkStacks.ContainsKey(perkKey))
        {
            Debug.Log("Contains");
            perkStacks[perkKey] = new List<Perk>();
        }
        
        perkStacks[perkKey].Add(perk);

        SpawnSlotUI(perk);
            
        // Apply effect with new stack count
        perk.ApplyPerkEffect(perkStacks[perkKey].Count);
        
        Debug.Log($"Added {perkKey}. Total stacks: {perkStacks[perkKey].Count}");
    }

    void SpawnSlotUI(Perk perk)
    {
        string perkKey = GetPerkKey(perk);
    
        if (!perkStacksUI.ContainsKey(perkKey))
        {
            perkStacksUI[perkKey] = new List<Perk>();
        }
    
        perkStacksUI[perkKey].Add(perk);
    
        // Clear and rebuild the entire sidebar
        RebuildSideBarUI();
    }
    
    public void RemoveFromInventorySideBar(string perkKey)
    {
        if (perkStacksUI.ContainsKey(perkKey) && perkStacksUI[perkKey].Count > 0)
        {
            // Remove the last added perk from UI tracking
            Perk perkToRemove = perkStacksUI[perkKey][perkStacksUI[perkKey].Count - 1];
            perkStacksUI[perkKey].RemoveAt(perkStacksUI[perkKey].Count - 1);
        
            // Remove empty stacks
            if (perkStacksUI[perkKey].Count == 0)
            {
                perkStacksUI.Remove(perkKey);
            }
        
            // Just rebuild the sidebar - don't try to find and destroy specific UI elements
            RebuildSideBarUI();
        }
    }
    
    void RebuildSideBarUI()
    {
        // Clear all current UI
        foreach (GameObject item in sideBarItemsUI)
        {
            Destroy(item);
        }
        sideBarItemsUI.Clear();
    
        // Rebuild all UI from scratch
        int i = 0;
        foreach (var perkStackUI in perkStacksUI)
        {
            Vector3 newPosition = slotUIExample.transform.position;
            GameObject newItem = Instantiate(slotUIExample, sideBarParent.transform);
        
            string displayName = perkStackUI.Key;
            if (perkStackUI.Value.Count > 1)
            {
                displayName += $" x{perkStackUI.Value.Count}";
            }
        
            newItem.name = displayName;
            newItem.transform.position = new Vector3(newPosition.x, newPosition.y - 100f * i, newPosition.z);
            
            Debug.Log(newItem.transform.GetChild(0).name);
            newItem.transform.GetChild(0).GetComponent<Image>().sprite = perkStackUI.Value[0].image;
            newItem.GetComponentInChildren<TextMeshProUGUI>().text = displayName;
            
            string currentPerkKey = perkStackUI.Key;
            
            newItem.SetActive(true);
            sideBarItemsUI.Add(newItem);
            i++;
        }
    }

    void ShowInventoryUI()
    {
        isInventoryOn = true;
        inventoryBackgroundUI.SetActive(true);
        int i = 0;
        
        foreach (var perkStack in perkStacks)
        {
            GameObject newItem = Instantiate(inventoryItemExampleUI, inventoryBackgroundUI.transform);
            
            string displayName = perkStack.Key;
            if (perkStack.Value.Count > 1)
            {
                displayName += $" x{perkStack.Value.Count}";
            }
            
            newItem.name = displayName;
            newItem.GetComponent<Image>().sprite = perkStack.Value[0].image;
            newItem.GetComponentInChildren<TextMeshProUGUI>().text = displayName;
            
            string perkKey = perkStack.Key;
            newItem.GetComponentInChildren<Button>().onClick.AddListener(() => CallDropItem(perkKey));
            newItem.SetActive(true);
            inventoryItemsUI.Add(newItem);
            i++;
        }

        int k = 1;
        foreach (GameObject uiItem in inventoryItemsUI)
        {
            Vector3 newPosition = inventoryItemExampleUI.transform.position;
            float totalWidth = Screen.width - 400;
            float startX = inventoryItemExampleUI.transform.position.x - totalWidth/2;
            startX = startX + totalWidth*k/(inventoryItemsUI.Count + 1);
            uiItem.transform.position = new Vector3(startX, newPosition.y, newPosition.z);
            k++;
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
            Debug.Log("here");
            // Remove the last added perk (most recent)
            Perk perkToRemove = perkStacks[perkKey][perkStacks[perkKey].Count - 1];
            perkStacks[perkKey].RemoveAt(perkStacks[perkKey].Count - 1);
            Debug.Log("there");
            
            // FIXED: Call RemovePerkEffect on the perk being removed
            int newStackCount = perkStacks.ContainsKey(perkKey) ? perkStacks[perkKey].Count : 0;
            perkToRemove.RemovePerkEffect(perkStacks[perkKey].Count);

            Debug.Log("blue");
            
            // Remove empty stacks
            if (perkStacks[perkKey].Count == 0)
            {
                perkStacks.Remove(perkKey);
                Debug.Log("removed");
            }
            else
            {
                // Reapply effects with new stack count for remaining perks
                perkStacks[perkKey][0].ApplyPerkEffect(perkStacks[perkKey].Count);
                Debug.Log("still some stwack left");
            }
            
            // Spawn the dropped perk in world
            Vector3 newPosition = transform.position + transform.forward * 5;
            newPosition.y = 0.5767174f;
            GameObject item = Instantiate(dropedItemExample, newPosition, Quaternion.identity, transform.parent);
            SpecificPerkItem itemComponent = item.GetComponent<SpecificPerkItem>();
            
            Debug.Log("aaa");
            if (itemComponent != null)
            {
                itemComponent.SetPerk(perkToRemove);
                Debug.Log("setting item perk");
            }
            
            int remainingStacks = perkStacks.ContainsKey(perkKey) ? perkStacks[perkKey].Count : 0;
            // Debug.Log($"Dropped {perkKey}. Remaining stacks: {remainingStacks}");
            Debug.Log("all done");
        }
    }
    
    public void CallDropItem(string perkKey)
    {
        HideInventoryUI();
        DropAndRemoveFromInventory(perkKey);
        RemoveFromInventorySideBar(perkKey);
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