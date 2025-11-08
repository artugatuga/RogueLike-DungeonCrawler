using UnityEngine;
using UnityEngine.InputSystem;

public class PerksManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private Perk[] perks; 
    void Start()
    {
        
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
