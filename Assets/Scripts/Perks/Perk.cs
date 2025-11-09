using System;
using UnityEngine;
using UnityEngine.UI;

public enum PerkType { Extra, Other };
public enum ExtraPerkType { None, Damage, Health, Defense, Crit, Speed };
public enum OtherPerkType { None, FlameTrail, EnemyExplode, DamageOverTime, SlowEnemies, LifeSteal, SizeAttack };

public class Perk : MonoBehaviour
{
    [field:SerializeField] public PerkType perkType { get; set; }
    [field:SerializeField] public ExtraPerkType extraPerkType { get; set; }
    [field:SerializeField] public OtherPerkType otherPerkType { get; set; }
    
    [SerializeField] private float baseModifier;

    [field:SerializeField] public Sprite image { get; set; }
    
    private PlayerManager playerManager;
    private TemporaryHealth playerHealth;
    
    public Action<int> perkApplyCallback {get; set; }
    public Action<int> perkRemoveCallback {get; set; }
    
    void Start()
    {
        playerManager = FindFirstObjectByType<PlayerManager>();
        playerHealth = FindFirstObjectByType<TemporaryHealth>();
        
        if (perkType == PerkType.Extra)
        {
            switch (extraPerkType)
            {
                case ExtraPerkType.Damage:
                    perkApplyCallback = ApplyDamage;
                    perkRemoveCallback = RemoveDamage;
                    break;
                case ExtraPerkType.Health:
                    perkApplyCallback = ApplyHealth;
                    perkRemoveCallback = RemoveHealth;
                    break;
                case ExtraPerkType.Defense:
                    perkApplyCallback = ApplyDefense;
                    perkRemoveCallback = RemoveDefense;
                    break;
                case ExtraPerkType.Crit:
                    perkApplyCallback = ApplyCrit;
                    perkRemoveCallback = RemoveCrit;
                    break;
                case ExtraPerkType.Speed:
                    perkApplyCallback = ApplySpeed;
                    perkRemoveCallback = RemoveSpeed;
                    break;
            }
        }
        else
        {
            switch (otherPerkType)
            {
                case OtherPerkType.FlameTrail:
                    perkApplyCallback = ApplyFlameTrail;
                    perkRemoveCallback = RemoveFlameTrail;
                    break;
                case OtherPerkType.EnemyExplode:
                    perkApplyCallback = ApplyEnemyExplode;
                    perkRemoveCallback = RemoveEnemyExplode;
                    break;
                case OtherPerkType.DamageOverTime:
                    perkApplyCallback = ApplyDamageOverTime;
                    perkRemoveCallback = RemoveDamageOverTime;
                    break;
                case OtherPerkType.SlowEnemies:
                    perkApplyCallback = ApplySlowEnemies;
                    perkRemoveCallback = RemoveSlowEnemies;
                    break;
                case OtherPerkType.LifeSteal:
                    perkApplyCallback = ApplyLifeSteal;
                    perkRemoveCallback = RemoveLifeSteal;
                    break;
                case OtherPerkType.SizeAttack:
                    perkApplyCallback = ApplySizeAttack;
                    perkRemoveCallback = RemoveSizeAttack;
                    break;
            }
        }
    }

    public void ApplyPerkEffect(int stackCount)
    {
        perkApplyCallback?.Invoke(stackCount);
    }
    
    public void RemovePerkEffect(int stackCount)
    {
        perkRemoveCallback?.Invoke(stackCount);
    }
    
    // Extra Perks
    void ApplyDamage(int stackCount)
    {
        if (playerManager)
        {
            float totalModifier = CalculateTotalModifier(stackCount);
            playerManager.damage = playerManager.initalDamage * (1 + totalModifier);
        }
    }
    
    void RemoveDamage(int stackCount)
    {
        if (playerManager)
        {
            float totalModifier = CalculateTotalModifier(stackCount);
            playerManager.damage = playerManager.initalDamage * (1 + totalModifier);
        }
    }
    
    void ApplyHealth(int stackCount)
    {
        if (playerHealth)
        {
            playerHealth.AddToMaxHealth(baseModifier * stackCount);
        }
    }
    
    void RemoveHealth(int stackCount)
    {
        if (playerHealth)
        {
            playerHealth.TakeFromMaxHealth(baseModifier);
        }
    }

    void ApplySpeed(int stackCount)
    {
        if (playerManager)
        {
            float totalModifier = CalculateTotalModifier(stackCount);
            playerManager.speed = playerManager.initialSpeed * (1 + totalModifier);
        }
    }    
    
    void RemoveSpeed(int stackCount)
    {
        if (playerManager)
        {
            float totalModifier = CalculateTotalModifier(stackCount);
            playerManager.speed = playerManager.initialSpeed * (1 + totalModifier);
        }
    }
    
    void ApplyCrit(int stackCount)
    {
        if (playerManager)
        {
            float totalModifier = CalculateTotalModifier(stackCount);
            playerManager.critDamage = playerManager.initalCritDamage * (1 + totalModifier);
        }
    }    
    
    void RemoveCrit(int stackCount)
    {
        if (playerManager)
        {
            float totalModifier = CalculateTotalModifier(stackCount);
            playerManager.critDamage = playerManager.initalCritDamage * (1 + totalModifier);
        }
    }
    
    void ApplyDefense(int stackCount)
    {
        if (playerManager)
        {
            float totalModifier = CalculateTotalModifier(stackCount);
            playerManager.armor = playerManager.initialArmor + (baseModifier * totalModifier);
        }
    }    
    
    void RemoveDefense(int stackCount)
    {
        if (playerManager)
        {
            float totalModifier = CalculateTotalModifier(stackCount);
            playerManager.armor = playerManager.initialArmor + (baseModifier * totalModifier);
        }
    }
    
    void ApplyLifeSteal(int stackCount)
    {
        if (playerManager)
        {
            float totalModifier = CalculateTotalModifier(stackCount);
            playerManager.lifeSteal = playerManager.initialLifeSeteal + (baseModifier * totalModifier);
        }
    }
    
    void RemoveLifeSteal(int stackCount)
    {
        if (playerManager)
        {
            float totalModifier = CalculateTotalModifier(stackCount);
            playerManager.lifeSteal = playerManager.initialLifeSeteal + (baseModifier * totalModifier);
        }
    }

    // Other Perks
    void ApplySizeAttack(int stackCount)
    {
        Debug.Log($"SizeAttack applied with {stackCount} stacks");
    }    
    
    void RemoveSizeAttack(int stackCount)
    {
        Debug.Log($"SizeAttack removed to {stackCount} stacks");
    }
    
    void ApplyFlameTrail(int stackCount)
    {
        Debug.Log($"FlameTrail applied with {stackCount} stacks");
    }
    
    void RemoveFlameTrail(int stackCount)
    {
        Debug.Log($"FlameTrail removed to {stackCount} stacks");
    }
    
    void ApplyDamageOverTime(int stackCount)
    {
        Debug.Log($"DamageOverTime applied with {stackCount} stacks");
    }    
    
    void RemoveDamageOverTime(int stackCount)
    {
        Debug.Log($"DamageOverTime removed to {stackCount} stacks");
    }
    
    void ApplySlowEnemies(int stackCount)
    {
        Debug.Log($"SlowEnemies applied with {stackCount} stacks");
    }    
    
    void RemoveSlowEnemies(int stackCount)
    {
        Debug.Log($"SlowEnemies removed to {stackCount} stacks");
    }
    
    void ApplyEnemyExplode(int stackCount)
    {
        Debug.Log($"EnemyExplode applied with {stackCount} stacks");
    }
    
    void RemoveEnemyExplode(int stackCount)
    {
        Debug.Log($"EnemyExplode removed to {stackCount} stacks");
    }
    
    private float CalculateTotalModifier(int stackCount)
    {
        float total = 0f;
        for (int i = 1; i <= stackCount; i++)
        {
            // FIXED: Your original formula - each stack gives baseModifier / sqrt(sqrt(stackNumber))
            total += baseModifier / Mathf.Sqrt(Mathf.Sqrt(i));
        }
        return total;
    }
    
    public string GetPerkName()
    {
        if (perkType == PerkType.Extra)
        {
            return extraPerkType.ToString();
        }
        else
        {
            return otherPerkType.ToString();
        }
    }
}