using System;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private AttackHitbox attackHitbox;
    [SerializeField] private PlayerCombat playerCombat;

    public void EnableHitbox()
    {
        attackHitbox.EnableHitbox();
    }

    public void DisableHitbox()
    {
        attackHitbox.DisableHitbox();
    }

    public void FinishAttack()
    {
        playerCombat.AttackFinished();
    }
}
