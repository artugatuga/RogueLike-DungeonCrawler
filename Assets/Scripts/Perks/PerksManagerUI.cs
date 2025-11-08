using System.Collections.Generic;
using UnityEngine;

public class PerksManagerUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [SerializeField] private GameObject perkMenuBackgroundUI;
    [SerializeField] private GameObject perkSelectionCardUI;
    [SerializeField] private List<Perk> perksSelectionCards;

    public void ChangePerks(List<Perk> perksSelected)
    {
        perkMenuBackgroundUI.SetActive(true);

        for (int i = 0; i < perksSelected.Count; i++)
        {
            GameObject currentPerk = Instantiate(perkSelectionCardUI, perkMenuBackgroundUI.transform);
            currentPerk.name = perksSelected[i].name;
            currentPerk.SetActive(true);
            perksSelectionCards.Add(perksSelected[i]);
        }
    }
}
