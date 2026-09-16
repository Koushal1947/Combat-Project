using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
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
        ProcessMovement();
        ProcessJump();
        
    }

    private void ProcessMovement()
    {

        if(charController.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 movement = Vector3.zero;

        if (leftMovementButton.IsPressed())
        {
            movement = Vector3.back * speed;
            animator.SetBool("walkBackward", true);
            animator.SetBool("walkForward", false);
        }

        else if (rightMovementButton.IsPressed())
        {
            movement = Vector3.forward * speed;
            animator.SetBool("walkBackward", false);
            animator.SetBool("walkForward", true);
        }

        else
        {
            animator.SetBool("walkBackward", false);
            animator.SetBool("walkForward", false);
        }

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

    
}
