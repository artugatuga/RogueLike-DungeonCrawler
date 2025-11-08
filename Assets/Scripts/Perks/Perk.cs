using System;
using UnityEngine;using UnityEngine.Rendering;


public enum PerkType {Extra, Other};
public enum ExtraPerkType {None, Damage, Health, Defense, Crit, Speed};

public enum OtherPerkType {None, FlameTrail, EnemyExplode, DamageOverTime, SlowEnemies, LifeSteal, SizeAttack};

public class Perk : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [SerializeField] private PerkType perkType;
    [SerializeField] private ExtraPerkType extraPerkType;
    [SerializeField] private OtherPerkType otherPerkType;
    
    private Action perkCallback;
    
    void Start()
    {
        if (perkType == PerkType.Extra)
        {
            switch (extraPerkType)
            {
                case ExtraPerkType.Damage:
                    perkCallback = ExtraDamage;
                    break;
                case ExtraPerkType.Health:
                    perkCallback = ExtraHealth;
                    break;
                case ExtraPerkType.Defense:
                    perkCallback = ExtraDefense;
                    break;
                case ExtraPerkType.Crit:
                    perkCallback = ExtraCrit;
                    break;
                case ExtraPerkType.Speed:
                    perkCallback = ExtraSpeed;
                    break;
            }
        }
        else
        {
            switch (otherPerkType)
            {
                case OtherPerkType.FlameTrail:
                    perkCallback = FlameTrail;
                    break;
                case OtherPerkType.EnemyExplode:
                    perkCallback = EnemyExplode;
                    break;
                case OtherPerkType.DamageOverTime:
                    perkCallback = DamageOverTime;
                    break;
                case OtherPerkType.SlowEnemies:
                    perkCallback = SlowEnemies;
                    break;
                case OtherPerkType.LifeSteal:
                    perkCallback = LifeSteal;
                    break;
                case OtherPerkType.SizeAttack:
                    perkCallback = SizeAttack;
                    break;
            }
        }
    }

    public void CallPerkFunction()
    {
        perkCallback?.Invoke();
    }
    
    //Extras
    
    void ExtraDamage()
    {
        Debug.Log(perkType);
        Debug.Log("ExtraDamage: " + extraPerkType);
    }
    void ExtraHealth()
    {
        Debug.Log(perkType);
        Debug.Log("ExtraHealth: " + extraPerkType);
    }

    void ExtraSpeed()
    {
        Debug.Log(perkType);
        Debug.Log("ExtraSpeed: " + extraPerkType);
    }
    void ExtraCrit()
    {
        Debug.Log(perkType);
        Debug.Log("ExtraCrit: " + extraPerkType);
    }
    
    void ExtraDefense()
    {
        Debug.Log(perkType);
        Debug.Log("ExtraDefense: " + extraPerkType);
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
