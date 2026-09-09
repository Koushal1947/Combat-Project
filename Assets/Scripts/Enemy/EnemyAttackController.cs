using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    [SerializeField] EnemyController enemyController;

    public void FinishAttack()
    {
        enemyController.AttackFinished();
    }
}
