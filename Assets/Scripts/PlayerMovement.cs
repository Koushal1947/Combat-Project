using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController charController;
    [SerializeField] private InputAction leftMovementButton;
    [SerializeField] private InputAction rightMovementButton;

    [Header("Movement")]
    [SerializeField] private float speed = 5;


    void Start()
    {
        leftMovementButton.Enable();
        rightMovementButton.Enable();
    }

    
    void Update()
    {
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        if (leftMovementButton.IsPressed())
        {
            charController.Move(Vector3.back * speed * Time.deltaTime);
            animator.SetBool("walkBackward", true);
            animator.SetBool("walkForward", false);
        }

        else if (rightMovementButton.IsPressed())
        {
            charController.Move(Vector3.forward * speed * Time.deltaTime);
            animator.SetBool("walkBackward", false);
            animator.SetBool("walkForward", true);
        }

        else
        {
            animator.SetBool("walkBackward", false);
            animator.SetBool("walkForward", false);
        }
    }
           
}
