using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Animator animator;
    

    [SerializeField] InputAction leftMovementButton;
    [SerializeField] InputAction rightMovementButton;

    [Header("Movement")]
    [SerializeField] float speed;


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

        if (rightMovementButton.IsPressed())
        {
            transform.position += new Vector3(0, 0, speed * Time.deltaTime);
            animator.SetBool("walkBackward", false);
            animator.SetBool("walkForward", true);
        }

        if (leftMovementButton.WasReleasedThisFrame() || rightMovementButton.WasReleasedThisFrame())
        {
            animator.SetBool("walkBackward", false);
            animator.SetBool("walkForward", false);
        }
    }
}
