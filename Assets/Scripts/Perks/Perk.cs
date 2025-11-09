using System;
using UnityEngine;using UnityEngine.Rendering;


public enum PerkType {Extra, Other};
public enum ExtraPerkType {None, Damage, Health, Defense, Crit, Speed};

public enum OtherPerkType {None, FlameTrail, EnemyExplode, DamageOverTime, SlowEnemies, LifeSteal, SizeAttack};

public class Perk : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [field:SerializeField] public PerkType perkType {get; set;}
    [field:SerializeField] public ExtraPerkType extraPerkType {get; set;}
    [field:SerializeField] public OtherPerkType otherPerkType {get; set;}
    
    [SerializeField] private float extraModifier;
    [SerializeField] private int timesModifierUsed;
    
    private PlayerManager playerManager;
    private TemporaryHealth playerHealth;
    
    private Action perkCallback;
    private Action perkRemoveCallback;
    
    void Start()
    {
        playerManager = FindFirstObjectByType<PlayerManager>();
        playerHealth = FindFirstObjectByType<TemporaryHealth>();
        
        if (perkType == PerkType.Extra)
        {
            switch (extraPerkType)
            {
                case ExtraPerkType.Damage:
                    perkCallback = ExtraDamage;
                    perkRemoveCallback = DropDamage;
                    break;
                case ExtraPerkType.Health:
                    perkCallback = ExtraHealth;
                    perkRemoveCallback = DropHealth;
                    break;
                case ExtraPerkType.Defense:
                    perkCallback = ExtraDefense;
                    perkRemoveCallback = DropDefense;
                    break;
                case ExtraPerkType.Crit:
                    perkCallback = ExtraCrit;
                    perkRemoveCallback = DropCrit;
                    break;
                case ExtraPerkType.Speed:
                    perkCallback = ExtraSpeed;
                    perkRemoveCallback = DropSpeed;
                    break;
            }
        }
        else
        {
            switch (otherPerkType)
            {
                case OtherPerkType.FlameTrail:
                    perkCallback = FlameTrail;
                    perkRemoveCallback = DropSpeed;
                    break;
                case OtherPerkType.EnemyExplode:
                    perkCallback = EnemyExplode;
                    perkRemoveCallback = DropSpeed;
                    break;
                case OtherPerkType.DamageOverTime:
                    perkCallback = DamageOverTime;
                    perkRemoveCallback = DropSpeed;
                    break;
                case OtherPerkType.SlowEnemies:
                    perkCallback = SlowEnemies;
                    perkRemoveCallback = DropSpeed;
                    break;
                case OtherPerkType.LifeSteal:
                    perkCallback = LifeSteal;
                    perkRemoveCallback = DropSpeed;
                    break;
                case OtherPerkType.SizeAttack:
                    perkCallback = SizeAttack;
                    perkRemoveCallback = DropSpeed;
                    break;
            }
        }
    }

    public void CallPerkFunction()
    {
        timesModifierUsed++;
        Debug.Log(extraPerkType);
        Debug.Log(perkCallback);
        perkCallback?.Invoke();
    }
    
    public void CallRemovePerkFunction()
    {
        timesModifierUsed--;
        Debug.Log(extraPerkType);
        perkRemoveCallback?.Invoke();
    }
    
    //Extras
    
    void ExtraDamage()
    {
        if (playerManager)
        {
            extraModifier = extraModifier/Mathf.Sqrt(Mathf.Sqrt(timesModifierUsed));
            playerManager.damage *= (1 + extraModifier);
        }
    }
    void DropDamage()
    {
        if (playerManager)
        {
            playerManager.damage /= (1 + extraModifier);
        }
    }
    
    void ExtraHealth()
    {
        if (playerHealth)
        {
            playerHealth.AddToMaxHealth(extraModifier);
        }
    }
    
    void DropHealth()
    {
        if (playerManager)
        {
            playerHealth.TakeFromMaxHealth(extraModifier);
        }
    }

    void ExtraSpeed()
    {
        if (playerManager)
        {
            extraModifier = extraModifier/Mathf.Sqrt(Mathf.Sqrt(timesModifierUsed));
            playerManager.speed *= (1 + extraModifier);
        }
    }    
    
    void DropSpeed()
    {
        if (playerManager)
        {
            playerManager.speed /= (1 + extraModifier);
        }
    }
    
    void ExtraCrit()
    {
        if (playerManager)
        {
            extraModifier = extraModifier/Mathf.Sqrt(Mathf.Sqrt(timesModifierUsed));
            playerManager.critDamage *= (1 + extraModifier);
        }
    }    
    void DropCrit()
    {
        if (playerManager)
        {
            playerManager.critDamage /= (1 + extraModifier);
        }
    }
    
    void ExtraDefense()
    {
        Debug.Log("Called:" + extraPerkType);
        
        if (playerManager)
        {
            Debug.Log(extraPerkType);
            playerManager.armor = 5;
            Debug.Log(playerManager.armor);
        }
    }    
    
    void DropDefense()
    {
        if (playerManager)
        {
            playerManager.armor /= (1 + extraModifier);
        }
    }
    
    //Others
    
    void LifeSteal()
    {
        Debug.Log(perkType);
        Debug.Log("LifeSteal: " + extraPerkType);
    }

    void SizeAttack()
    {
        Debug.Log(perkType);
        Debug.Log("SizeAttack: " + extraPerkType);
    }    
    
    void FlameTrail()
    {
        Debug.Log(perkType);
        Debug.Log("FlameTrail: " + extraPerkType);
    }
    
    void DamageOverTime()
    {
        Debug.Log(perkType);
        Debug.Log("DamageOverTime: " + extraPerkType);
    }    
    
    void SlowEnemies()
    {
        Debug.Log(perkType);
        Debug.Log("SlowEnemies: " + extraPerkType);
    }    
    
    void EnemyExplode()
    {
        Debug.Log(perkType);
        Debug.Log("EnemyExplode: " + extraPerkType);
    }
}
