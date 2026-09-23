using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerCombat playerCombat;
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController charController;
    [SerializeField] private InputAction leftMovementButton;
    [SerializeField] private InputAction rightMovementButton;
    [SerializeField] private InputAction jumpButton;
    
    [SerializeField] private float gravity = -9.81f;

    private float verticalVelocity;

    [Header("Movement")]
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 10;


    void Start()
    {
        leftMovementButton.Enable();
        rightMovementButton.Enable();
        jumpButton.Enable();
        
    }

    
    void Update()
    {
        if (!playerCombat.IsAttacking)
        {
            ProcessMovement();
            ProcessJump();
        }

        ApplyGravity();
    }

    private void ProcessMovement()
    {
        float targetMoveAmount = 0f;

       

        Vector3 movement = Vector3.zero;

        if (leftMovementButton.IsPressed())
        {
            movement = Vector3.back * speed;
            targetMoveAmount = -1f;
        }

        else if (rightMovementButton.IsPressed())
        {
            movement = Vector3.forward * speed;
            targetMoveAmount = 1f;
        }

        else
        {
            targetMoveAmount = 0f;
        }

        animator.SetFloat("MoveAmount", targetMoveAmount, 0.1f, Time.deltaTime);

        movement.y = verticalVelocity;

        charController.Move(movement * Time.deltaTime);
    }
           

    void ProcessJump()
    {
        if (jumpButton.WasPressedThisFrame() && charController.isGrounded)
        {
            animator.SetTrigger("Jump");
            verticalVelocity = jumpForce;
        }
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
}
