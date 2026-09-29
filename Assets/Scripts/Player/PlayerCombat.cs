using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private InputAction attackButton;
    [SerializeField] private CharacterController charController;
    [SerializeField] private PlayerMovement playerMovement;

    [Header("Attack")]
    [SerializeField] private int attack1Damage = 20;
    [SerializeField] private int attack2Damage = 25;
    [SerializeField] private int attack3Damage = 35;

    [Header("Hitstun")]
    [SerializeField] private float attack1Hitstun = 0.15f;
    [SerializeField] private float attack2Hitstun = 0.2f;
    [SerializeField] private float attack3Hitstun = 0.35f;

    [Header("Knockback")]
    [SerializeField] private float attack1Knockback = 1f;
    [SerializeField] private float attack2Knockback = 2f;
    [SerializeField] private float attack3Knockback = 4f;

    [Header("Hitstop")]
    [SerializeField] private float attack1Hitstop = 0.03f;
    [SerializeField] private float attack2Hitstop = 0.04f;
    [SerializeField] private float attack3Hitstop = 0.07f;

    [Header("AttackMovement")]
    [SerializeField] private float attack1Movespeed = 1.5f;
    [SerializeField] private float attack2Movespeed = 2f;
    [SerializeField] private float attack3Movespeed = 3.5f;

    private int comboStep = 0;

    public bool IsAttacking => isAttacking;
    public bool AttackMovementActive => attackMovementActive;
    private bool isAttacking = false;
    private bool canQueueNextAttack;
    private bool attackQueued;
    private bool attackMovementActive;
    


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

        playerMovement.SetAttackDirection();

        animator.SetTrigger("Attack");
    }

    

    public void AttackFinished()
    {
        isAttacking = false;

        comboStep = 0;
        canQueueNextAttack = false;
        attackQueued = false;

        animator.SetTrigger("EndCombo");
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
        else
        {
            AttackFinished();
        }
    }

    private void AdvanceCombo()
    {
        comboStep++;

        playerMovement.SetAttackDirection();

        animator.SetTrigger("NextAttack");
    }

    public int GetCurrentDamage()
    {
        switch (comboStep)
        {
            case 1:
                return attack1Damage;
            case 2:
                return attack2Damage;
            case 3:
                return attack3Damage;

            default:
                return 0;
        }

    }

    public float GetCurrentHitstun()
    {
        switch (comboStep)
        {
            case 1:
                return attack1Hitstun;
            case 2:
                return attack2Hitstun;
            case 3:
                return attack3Hitstun;
            default:
                return 1.5f;

        }
    }

    public float GetCurrentKnockback()
    {
        switch (comboStep)
        {
            case 1:
                return attack1Knockback;
            case 2:
                return attack2Knockback;
            case 3:
                return attack3Knockback;
            default:
                return 0;

        }
    }

    public float GetCurrentHitstop()
    {
        switch (comboStep)
        {
            case 1:
                return attack1Hitstop;
            case 2:
                return attack2Hitstop;
            case 3:
                return attack3Hitstop;
            default:
                return 0.03f;
        }
    }

    public float GetCurrentMoveSpeed()
    {
        switch (comboStep)
        {
            case 1:
                return attack1Movespeed;
            case 2:
                return attack2Movespeed;
            case 3:
                return attack3Movespeed;
            default:
                return 1.5f;
        }
    }
    
    public int GetCurrentComboStep()
    {
        return comboStep;
    }

    public void StartAttackMovement()
    {
        attackMovementActive = true;
    }

    public void StopAttackMovement()
    {
        attackMovementActive = false;
    }

}
