using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PerksManagerUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [SerializeField] private GameObject perkMenuBackgroundUI;
    [SerializeField] private GameObject perkSelectionCardUI;
    [SerializeField] private List<Perk> perksSelectionCards;
    [SerializeField] private List<GameObject> perksSelectionCardsUI;
    [SerializeField] private GameManager gameManager;
    private Inventory inventory;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        inventory = FindFirstObjectByType<Inventory>();
    }

    public void ChangePerks(List<Perk> perksSelected)
    {
        for (int i = 0; i < perksSelected.Count; i++)
        {
            GameObject currentPerk = Instantiate(perkSelectionCardUI, perkMenuBackgroundUI.transform.parent);
            currentPerk.name = perksSelected[i].name;
            currentPerk.SetActive(true);

            float size = 1200;
            
            RectTransform parentRect = perkMenuBackgroundUI.GetComponent<RectTransform>();
            Vector3 parentPosition = parentRect.position;
            Vector3 newPosition = new Vector3(parentPosition.x - (size/perksSelected.Count * (i - 1)), parentPosition.y, parentPosition.z);
            
            currentPerk.transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);
            currentPerk.GetComponentInChildren<TextMeshProUGUI>().text = currentPerk.name;
            
            int index = i;
            currentPerk.GetComponentInChildren<Button>().onClick.AddListener(() => SelectPerk(index));
            
            perksSelectionCards.Add(perksSelected[i]);
            perksSelectionCardsUI.Add(currentPerk);
        }
        
        perkMenuBackgroundUI.SetActive(true);
    }

    public void SelectPerk(int perkIndex)
    {
        perkMenuBackgroundUI.SetActive(false);
        inventory.AddToInventory(perksSelectionCards[perkIndex]);
        
        foreach (GameObject perk in perksSelectionCardsUI)
        {
            Destroy(perk); 
        }
        
        perksSelectionCardsUI.RemoveRange(0, perksSelectionCardsUI.Count);
        perksSelectionCards.RemoveRange(0, perksSelectionCards.Count);
        gameManager.PauseGame();
    }
}
