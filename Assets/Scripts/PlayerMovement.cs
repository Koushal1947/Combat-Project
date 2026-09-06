using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;

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
            transform.position += new Vector3(0, 0, -speed * Time.deltaTime);
            animator.SetBool("walkBackward", true);
            animator.SetBool("walkForward", false);
        }

        else if (rightMovementButton.IsPressed())
        {
            transform.position += new Vector3(0, 0, speed * Time.deltaTime);
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
