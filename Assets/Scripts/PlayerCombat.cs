using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private InputAction attackButton;

    private bool isAttacking = false;

    void Start()
    {
        attackButton.Enable();
    }

    void Update()
    {
        ProcessAttack();
    }

    void ProcessAttack()
    {
        if (!isAttacking && attackButton.WasPressedThisFrame())
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
        }
    }

    public void AttackFinished()
    {
        isAttacking = false;
    }
}
