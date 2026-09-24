using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Transform lockOnTarget;

    [Header("General")]
    [SerializeField] private float distance = 5f;
    [SerializeField] private float mouseSensitivity = 2f;

    [Header("Lock-On")]
    [SerializeField] private float lockOnYawSpeed = 5f;
    [SerializeField] private float lockOnPitchSpeed = 5f;
    [SerializeField] private float lookSmoothSpeed = 5f;
    [SerializeField] private float cameraBias = 0.35f;
    [SerializeField] private float lockOnPitch = 15f;

    
    private float yaw;
    private float pitch = 15f;


    private void LateUpdate()
    {

        if (playerMovement.IsLockedOn)
        {
            HandleLockOnCamera();
        }
        else
        {
            HandleFreeCamera();
        }
    }

    private void HandleLockOnCamera()
    {

        if(lockOnTarget == null)
        {
            return;
        }

        Vector3 directionToTarget = lockOnTarget.position - target.position;
        directionToTarget.y = 0f;

        if(directionToTarget.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            float targetYaw = targetRotation.eulerAngles.y;

            yaw = Mathf.LerpAngle(yaw, targetYaw, lockOnYawSpeed * Time.deltaTime);

            pitch = Mathf.Lerp(pitch, lockOnPitch, lockOnPitchSpeed * Time.deltaTime);
        }

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        Vector3 offset = rotation * new Vector3(0, 0, -distance);

        transform.position = target.position + offset;

        

        Vector3 lookPoint = Vector3.Lerp(target.position, lockOnTarget.position, cameraBias);

        transform.LookAt(lookPoint);

    }

    private void HandleFreeCamera()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        yaw += mouseDelta.x * mouseSensitivity;
        pitch -= mouseDelta.y * mouseSensitivity;

        pitch = Mathf.Clamp(pitch, -20f, 60f);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        Vector3 offset = rotation * new Vector3(0f, 0f, -distance);

        transform.position = target.position + offset;

        transform.LookAt(target);
    }
}
