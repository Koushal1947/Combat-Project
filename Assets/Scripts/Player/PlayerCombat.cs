using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{

    

    [System.Serializable]
    public class AttackData
    {
        public int damage;
        public float hitstun;
        public float knockback;
        public float hitstop;
        public float moveSpeed;

        public bool usesRootMotion;
        public bool causesKnockdown;

        public HitReactionType reactionType;
    }

    

    private enum Attacktype
    {
        None,
        Light1,
        Light2,
        Light3,
        Heavy

    }

    private Attacktype currentAttack = Attacktype.None; 

    [SerializeField] private Animator animator;
    [SerializeField] private InputAction lightAttackButton;
    [SerializeField] private InputAction heavyAttackButton;
    [SerializeField] private CharacterController charController;
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private AttackData light1Data;
    [SerializeField] private AttackData light2Data;
    [SerializeField] private AttackData light3Data;
    [SerializeField] private AttackData heavyData;


    private int comboStep = 0;


    public bool CurrentAttackUsesRotation => currentAttackData != null && currentAttackData.usesRootMotion;
    public bool CurrentAttackCausesKnockdown => currentAttackData.causesKnockdown;

    public bool IsAttacking => isAttacking;
    public bool AttackMovementActive => attackMovementActive;
    private bool isAttacking = false;
    private bool canQueueNextAttack;
    private bool attackQueued;
    private bool attackMovementActive;

    private AttackData currentAttackData;

    void Start()
    {
        lightAttackButton.Enable();
        heavyAttackButton.Enable();
    }

    void Update()
    {
        ProcessLightAttack();
        ProcessHeavyAttack();
    }

    void ProcessLightAttack()
    {

        if (!lightAttackButton.WasPressedThisFrame())
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

    void ProcessHeavyAttack()
    {
        if (!heavyAttackButton.WasPressedThisFrame())
        {
            return;
        }

        if (isAttacking)
        {
            return;
        }

        if (!charController.isGrounded)
        {
            return;
        }


        isAttacking = true;
        currentAttack = Attacktype.Heavy;
        currentAttackData = heavyData;

        playerMovement.SetAttackDirection();

        animator.SetTrigger("HeavyAttack");

    }

    private void StartCombo()
    {
        isAttacking = true;
        comboStep = 1;

        currentAttack = Attacktype.Light1;
        currentAttackData = light1Data;

        playerMovement.SetAttackDirection();

        animator.SetTrigger("Attack");
    }

    

    public void AttackFinished()
    {

        isAttacking = false;

        comboStep = 0;
        canQueueNextAttack = false;
        attackQueued = false;

        currentAttack = Attacktype.None;

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

        switch (comboStep)
        {
            case 2:
                currentAttack = Attacktype.Light2;
                currentAttackData = light2Data;
                break;

            case 3:
                currentAttack = Attacktype.Light3;
                currentAttackData = light3Data;
                break;
        }

        playerMovement.SetAttackDirection();

        animator.SetTrigger("NextAttack");
    }

    public int GetCurrentDamage()
    {
        return currentAttackData.damage;

    }

    public float GetCurrentHitstun()
    {
        return currentAttackData.hitstun;
    }

    public float GetCurrentKnockback()
    {
        return currentAttackData.knockback;
    }

    public float GetCurrentHitstop()
    {
        return currentAttackData.hitstop;
    }

    public float GetCurrentMoveSpeed()
    {
        return currentAttackData.moveSpeed;
    }

    public HitReactionType GetCurrentReactionType()
    {
        return currentAttackData.reactionType;
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
