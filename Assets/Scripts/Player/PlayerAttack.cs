using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject hitBox;
    private float attackCooldown = 2;
    private float attackTimer;
    private bool canAttack = true;

    // Update is called once per frame
    void Update()
    {
        Attack();
        Timers();
    }

    void Attack()
    {
        if (Mouse.current.leftButton.isPressed && canAttack)
        {
            hitBox.SetActive(true);
            canAttack = false;
        }
    }

    void Timers()
    {
        if (!canAttack)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= 0.4f)
            {
                hitBox.SetActive(false);
            }

            if (attackTimer >= attackCooldown)
            {
                canAttack = true;
                attackTimer = 0;
            }
        }
    }
}
