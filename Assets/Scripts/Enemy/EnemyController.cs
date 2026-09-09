using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private enum EnemyState
    {
        Idle,
        Approach,
        Attack,

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

    [Header("Ranges")]
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float attackRange = 1.5f;


    private void Start()
    {
        currentState = EnemyState.Idle;
    }

    private void Update()
    {

        

        if(currentState == EnemyState.Hitstun || currentState == EnemyState.Dead)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if(distanceToPlayer > detectionRange)
        {
            currentState = EnemyState.Idle;
        }
        else if(distanceToPlayer > attackRange)
        {
            currentState = EnemyState.Approach;
        }

        else
        {
            currentState = EnemyState.Attack;
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
        //move towards player
        animator.SetBool("walkForward", true);
        animator.SetBool("walkBackward", false);

        float direction = Mathf.Sign(player.position.z - transform.position.z);

        Vector3 movement = Vector3.forward * direction * moveSpeed;

        charController.Move(movement * Time.deltaTime);
    }

    void HandleAttack()
    {

        if (isAttacking)
        {
            return;
        }

        isAttacking = true;
        animator.SetTrigger("Attack");
    }

    void HandleHitstun()
    {

    }

    void HandleDead()
    {

    }

    public void AttackFinished()
    {
        isAttacking = false;
    }

}
