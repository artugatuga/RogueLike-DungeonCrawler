using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private static readonly int Attacking = Animator.StringToHash("Attacking");
    private static readonly int CanPlayAnimation = Animator.StringToHash("CanPlayAnimation");
    [SerializeField] private GameObject hitBox;
    private float attackCooldown = 2;
    private float attackTimer;
    private bool canAttack = true;
    
    [SerializeField]private AudioSource audioSource;
    [SerializeField]private AudioClip swordSound;
    
    [SerializeField] Animator animator;


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
            audioSource.clip = swordSound;
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.Play();
            
            animator.SetBool(CanPlayAnimation, true);
            hitBox.SetActive(true);
            canAttack = false;
            animator.SetTrigger(Attacking);
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

    public void OnAttackEnd()
    {
        animator.SetBool(CanPlayAnimation, false);
    }
}
