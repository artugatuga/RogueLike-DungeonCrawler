using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [field:SerializeField]
    public float maxHealth { get; set; } = 100;
    
    [field:SerializeField]
    public float initialArmor { get; set; } = 0;
    [field:SerializeField]
    public float armor { get; set; } = 0;
    
    [field:SerializeField]
    public float initalDamage { get; set; } = 25;
    [field:SerializeField]
    public float damage { get; set; } = 25;
    
    [field:SerializeField]
    public float initalCritDamage { get; set; } = 25;
    [field:SerializeField]
    public float critDamage { get; set; } = 25;
    
    [field:SerializeField]
    public float initialSize { get; set; } = 1;
    [field:SerializeField]
    public float size { get; set; } = 1;
    
    [field:SerializeField]
    public float initialSpeed { get; set; } = 12f;
    [field:SerializeField]
    public float speed { get; set; } = 12f;
    
    [field:SerializeField]
    public float initialLifeSeteal{ get; set; } = 12f;
    [field:SerializeField]
    public float lifeSteal { get; set; } = 12f;
}
