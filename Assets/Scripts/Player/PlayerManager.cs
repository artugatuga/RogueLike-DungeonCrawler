using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int health { get; set; } = 100;
    public int armor { get; set; } = 0;
    public int damage { get; set; } = 25;
    public int size { get; set; } = 1;
    public float speed { get; set; } = 12f;
}
