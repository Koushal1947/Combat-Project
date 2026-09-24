using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private InputAction attackButton;
    [SerializeField] private CharacterController charController;

    private int comboStep = 0;

    public bool IsAttacking => isAttacking;
    private bool isAttacking = false;
    private bool canQueueNextAttack;
    private bool attackQueued;

    


    void Start()
    {
        //attackButton.Enable();
    }

    void Update()
    {
        ProcessAttack();
    }

    void ProcessAttack()
    {

        if (!attackButton.WasPressedThisFrame())
        {
            return;
        }

        if (!charController.isGrounded)
        {
            return;
        }

        if (!isAttacking)
        {
            StartCombo();
        }
        else if (canQueueNextAttack)
        {
            attackQueued = true;
        }
    }

    private void StartCombo()
    {
        isAttacking = true;
        comboStep = 1;

        animator.SetTrigger("Attack");
    }

    public void AttackFinished()
    {
        isAttacking = false;

        comboStep = 0;
        canQueueNextAttack = false;
        attackQueued = false;
    }

    public void OpenComboWindow()
    {
        canQueueNextAttack = true;
    }

    public void CloseComboWindow()
    {
        canQueueNextAttack = false;

        if (attackQueued)
        {
            attackQueued = false;
            AdvanceCombo();
        }
    }

    private void AdvanceCombo()
    {
        comboStep++;

        animator.SetTrigger("NextAttack");
    }
}
