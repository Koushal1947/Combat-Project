using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerCombat playerCombat;
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController charController;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform lockOnTarget;


    [SerializeField] private InputAction lockOnButton;
    [SerializeField] private InputAction moveInput;
    [SerializeField] private InputAction jumpButton;
    
    [SerializeField] private float gravity = -9.81f;

    private float verticalVelocity;

    [Header("Movement")]
    [SerializeField] private float freeMoveSpeed = 5;
    [SerializeField] private float lockOnMoveSpeed = 4f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float jumpForce = 10;

    private bool isLockedOn;

    public bool IsLockedOn => isLockedOn;

    void Start()
    {
        jumpButton.Enable();
        moveInput.Enable();
        lockOnButton.Enable();
    }

    
    void Update()
    {
        if (!playerCombat.IsAttacking)
        {
            ProcessMovement();
            ProcessJump();
        }

        ProcessLockOn();
        ApplyGravity();
    }

    private void ProcessMovement()
    {

        Vector2 input = moveInput.ReadValue<Vector2>();

        Vector3 moveDirection;

        if (isLockedOn)
        {
            moveDirection = GetLockOnMovement(input);
        }
        else
        {
            moveDirection = GetFreeMovement(input);
        }
        
        

        if (!isLockedOn)
        {
            FaceMovementDirection(moveDirection);
        }

        float currentSpeed = isLockedOn ? lockOnMoveSpeed : freeMoveSpeed;

        charController.Move(moveDirection * currentSpeed * Time.deltaTime);

             
    }


    void ProcessJump()
    {
        if (jumpButton.WasPressedThisFrame() && charController.isGrounded)
        {
            animator.SetTrigger("Jump");
            verticalVelocity = jumpForce;
        }
    }

    void ProcessLockOn()
    {
        if (lockOnButton.WasPressedThisFrame())
        {
            isLockedOn = !isLockedOn;
            animator.SetBool("IsLockedOn", isLockedOn);
        }

        if (isLockedOn)
        {
            FaceTarget();
        }
        
    }

    void FaceMovementDirection(Vector3 moveDirection)
    {

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

    }

    void FaceTarget()
    {

        if(lockOnTarget == null)
        {
            return;
        }

        Vector3 direction = lockOnTarget.position - transform.position;
        direction.y = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void ApplyGravity()
    {
        if (charController.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 gravityMovement = Vector3.up * verticalVelocity;

        charController.Move(gravityMovement * Time.deltaTime);
    }

    private Vector3 GetLockOnMovement(Vector2 input)
    {
        if(lockOnTarget == null)
        {
            return Vector3.zero;
        }

        Vector3 forward = lockOnTarget.position - transform.position;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = Vector3.Cross(Vector3.up, forward);

        Vector3 direction = forward * input.y + 
                            right * input.x;

        if(direction.sqrMagnitude > 1f)
        {
            direction.Normalize();
        }

        animator.SetFloat("MoveX", input.x, 0.1f, Time.deltaTime);
        animator.SetFloat("MoveY", input.y, 0.1f, Time.deltaTime);

        return direction;
    }

    private Vector3 GetFreeMovement(Vector2 input)
    {
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;


        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDirection = cameraForward * input.y + cameraRight * input.x;

        if (moveDirection.sqrMagnitude > 1f)
        {
            moveDirection.Normalize();
        }

        animator.SetFloat("MoveAmount", input.magnitude, 0.1f, Time.deltaTime);

        return moveDirection;
    }

}
