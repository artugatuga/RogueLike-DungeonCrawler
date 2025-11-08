using System.Collections.Generic;
using UnityEngine;

public class PerksManagerUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [SerializeField] private GameObject perkMenuBackgroundUI;
    [SerializeField] private GameObject[] perksSelectionCardsUI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangePerks(List<Perk> perksSelected)
    {
        foreach (Perk perk in perksSelected)
        {
            Debug.Log(perk.name);
        }
    }
}
