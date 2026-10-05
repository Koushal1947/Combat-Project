using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRootMotion : MonoBehaviour
{
    [SerializeField] PlayerCombat playerCombat;
    [SerializeField] CharacterController charController;
    [SerializeField] Animator animator;
    [SerializeField] Transform playerRoot;

    private void OnAnimatorMove()
    {
        if (!playerCombat.CurrentAttackUsesRotation)
        {
            return;
        }

        charController.Move(animator.deltaPosition);

        playerRoot.rotation *= animator.deltaRotation;
    }

}
