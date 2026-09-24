using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private enum EnemyState
    {
        Idle,
        Approach,
        Attack,
        Recovery,
        Hitstun,
        Dead
    }

    private EnemyState currentState;

    private bool isAttacking;

    [SerializeField] private CharacterController charController;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform player;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 15f;

    [Header("Ranges")]
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float attackRange = 1.5f;


    [Header("Recovery")]
    [SerializeField] private float recoveryTime = 0.6f;
    private float recoveryTimer;

    private void Start()
    {
        currentState = EnemyState.Idle;
    }

    private void Update()
    {

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (currentState == EnemyState.Idle ||
            currentState == EnemyState.Approach ||
            currentState == EnemyState.Attack)
        {
            if (distanceToPlayer > detectionRange)
            {
                currentState = EnemyState.Idle;
            }
            else if (distanceToPlayer > attackRange)
            {
                currentState = EnemyState.Approach;
            }

            else
            {
                currentState = EnemyState.Attack;
            }
        }


        switch (currentState)
        {
            case EnemyState.Idle:
                HandleIdle();
                break;

            case EnemyState.Approach:
                HandleApproach();
                break;

            case EnemyState.Attack:
                HandleAttack();
                break;

            case EnemyState.Recovery:
                HandleRecovery();
                break;

            case EnemyState.Hitstun:
                HandleHitstun();
                break;

            case EnemyState.Dead:
                HandleDead();
                break;
        }
    }

    void HandleIdle()
    {
        animator.SetBool("walkForward", false);
        animator.SetBool("walkBackward", false);
    }

    void HandleApproach()
    {
        animator.SetBool("walkForward", true);
        animator.SetBool("walkBackward", false);

        Vector3 direction = player.position - transform.position;

        direction.y = 0f;
        direction.Normalize();

        FacePlayer();

        charController.Move(direction * moveSpeed * Time.deltaTime);
    }

    void HandleAttack()
    {
        FacePlayer();

        if (isAttacking)
        {
            return;
        }

        isAttacking = true;
        animator.SetTrigger("Attack");
    }

    void HandleRecovery()
    {
        animator.SetBool("walkForward", false);
        animator.SetBool("walkBackward", false);

        recoveryTimer -= Time.deltaTime;

        if(recoveryTimer <= 0)
        {
            currentState = EnemyState.Idle;
        }
    }

    void HandleHitstun()
    {
        animator.SetBool("walkForward", false);
        animator.SetBool("walkBackward", false);
    }

    void HandleDead()
    {

    }

    private void FacePlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        direction.Normalize();

        if (direction.sqrMagnitude > 0.01)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
        }
    }

    public void AttackFinished()
    {
        isAttacking = false;
        currentState = EnemyState.Recovery;
        recoveryTimer = recoveryTime;
    }

    public void EnterHitstun()
    {
        isAttacking = false;
        currentState = EnemyState.Hitstun;
    }

    public void ExitHitstun()
    {
        currentState = EnemyState.Idle;
    }

}
