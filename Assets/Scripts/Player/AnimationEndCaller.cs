using UnityEngine;

public class AnimationEndCaller : MonoBehaviour
{
    [SerializeField] private PlayerAttack PlayerAttack;
    public void OnAttackEnd()
    {
        PlayerAttack.OnAttackEnd();
    }
}
