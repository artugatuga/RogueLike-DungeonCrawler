using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PerksManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private Perk[] perks;
    [SerializeField] private int maxPerksOnSelection;
    [SerializeField] private PerksManagerUI managerUI;
    
    void Start()
    {
        RandomizePerkSelection();
    }
    
    void RandomizePerkSelection()
    {
        int[] perksSelected = new int[maxPerksOnSelection];
        List<int> perksAlreadySelected = new List<int>();
        List<int> perksSelectedList = new List<int>();
        
        for (int i = 0; i < maxPerksOnSelection; i++)
        {
            if (!perksAlreadySelected.Contains(perksSelected[i]))
            {
                int randomNum = Random.Range(0, perks.Length);
                perksSelected[i] = randomNum;
                perksSelectedList.Add(perksSelected[i]);
                perksAlreadySelected.Add(randomNum);
            }
            else
            {
                i--;
            }
        }
        
        ShowPerkSelection(perksSelectedList);
    }

    void ShowPerkSelection(List<int> perksSelected)
    {
        List<Perk> selectedPerksList = new List<Perk>();

        foreach (int perk in perksSelected)
        {
            selectedPerksList.Add(perks[perk]);
        }
        
        managerUI.ChangePerks(selectedPerksList);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("Pressed E");
            perks[0].CallPerkFunction();
        }
    }
}
