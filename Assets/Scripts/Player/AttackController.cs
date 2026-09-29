using UnityEngine;
using System.Collections.Generic;

public class AttackController : MonoBehaviour
{
    [SerializeField] private PlayerCombat playerCombat;
    [SerializeField] private AttackHitbox leftHitbox;
    [SerializeField] private AttackHitbox rightHitbox;

    private HashSet<EnemyHealth> hitEnemies = new HashSet<EnemyHealth>();

    public bool TryRegisterHit(EnemyHealth enemy)
    {
        if (hitEnemies.Contains(enemy))
        {
            return false;
        }

        hitEnemies.Add(enemy);
        return true;
    }

    public void ResetHitTargets()
    {
        hitEnemies.Clear();
    }

    public void EnableLeftHitbox()
    {
        leftHitbox.EnableHitbox();
    }

    public void DisableLeftHitbox()
    {
        leftHitbox.DisableHitbox();
    }

    public void EnableRightHitbox()
    {
        rightHitbox.EnableHitbox();
    }

    public void DisableRightHitbox()
    {
        rightHitbox.DisableHitbox();
    }

    public void FinishAttack()
    {
        playerCombat.AttackFinished();
    }

    public void OpenComboWindow()
    {
        playerCombat.OpenComboWindow();
    }

    public void CloseComboWindow()
    {
        playerCombat.CloseComboWindow();
    }

    public void StartAttackMovement()
    {
        playerCombat.StartAttackMovement();
    }

    public void StopAttackMovement()
    {
        playerCombat.StopAttackMovement();
    }
}
