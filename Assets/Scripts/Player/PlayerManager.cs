using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int health { get; set; } = 100;
    
    public int initialArmor { get; set; } = 0;
    public int armor { get; set; } = 0;
    
    public int initalDamage { get; set; } = 25;
    public int damage { get; set; } = 25;
    
    public int initialSize { get; set; } = 1;
    public int size { get; set; } = 1;
    
    public float initialSpeed { get; set; } = 12f;
    public float speed { get; set; } = 12f;
}
