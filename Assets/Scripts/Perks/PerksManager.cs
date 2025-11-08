using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PerksManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private Perk[] perks;
    [SerializeField] private int maxPerksOnSelection;
    [SerializeField] private PerksManagerUI managerUI;
    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void RandomizePerkSelection()
    {
        int[] perksSelected = new int[maxPerksOnSelection];
        List<int> perksAlreadySelected = new List<int>();
        List<int> perksSelectedList = new List<int>();

        int tries = 0;
        
        for (int i = 0; i < maxPerksOnSelection; i++)
        {
            int randomNum = Random.Range(0, perks.Length);
            if (!perksAlreadySelected.Contains(randomNum))
            {
                Debug.Log(randomNum);
                perksSelected[i] = randomNum;
                perksSelectedList.Add(perksSelected[i]);
                perksAlreadySelected.Add(randomNum);
            }
            else if (tries <= 1000)
            {
                i--;
                tries++;
            }
            else
            {
                Debug.LogError("To many tries");
                break;
            }
        }
        
        gameManager.PauseGame();
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

            if (gameManager != null)
            {
                
            }
        }
    }
}
