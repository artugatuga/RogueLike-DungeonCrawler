using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private Slider healthBar;

    private GameObject player;
    private PlayerManager playerManager;
    private TemporaryHealth temporaryHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBar = GetComponent<Slider>();
        player = GameObject.FindGameObjectWithTag("Player");
        temporaryHealth = player.GetComponent<TemporaryHealth>();
        playerManager = player.GetComponent<PlayerManager>();
        temporaryHealth.OnHealthChanged.AddListener(OnHealthChanged);
        OnHealthChanged();
    }


    void OnHealthChanged()
    {
        healthBar.maxValue = playerManager.maxHealth;
        healthBar.value = temporaryHealth.GetPlayerHealth();
    }
}
