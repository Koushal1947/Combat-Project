using UnityEngine;
using Unity.Collections;
using System.Collections;

public class EnemyReaction : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private Animator animator;
    private bool isHitStun;
    private Coroutine hitStunCoroutine;

    public bool isInHitStun => isHitStun;
    


    public void PlayHitReaction(float hitStunTime, float knockbackForce, Transform attacker, int comboStep)
    {
        if(hitStunCoroutine != null)
        {
            StopCoroutine(hitStunCoroutine);
        }

        if(comboStep == 3)
        {
            enemyController.EnterKnockdown();
        }
        else
        {
            enemyController.EnterHitstun();
        }

        PlayReactionAnimation(comboStep);

        bool isKnockdown = comboStep == 3;

        hitStunCoroutine = StartCoroutine(HitStun(hitStunTime,knockbackForce,attacker, isKnockdown));
    }

    private IEnumerator HitStun(float hitStunTime, float knockbackForce, Transform attacker, bool isKnockdown)
    {
        isHitStun = true;
        enemyController.EnterHitstun();

        Vector3 knockbackDirection = transform.position - attacker.position;

        knockbackDirection.y = 0f;
        knockbackDirection.Normalize();

        float timer = 0f;

        while (timer < hitStunTime)
        {
            enemyController.MoveKnockback(knockbackDirection, knockbackForce);

            timer += Time.deltaTime;
            yield return null;
        }

        if (!isKnockdown)
        {
            isHitStun = false;
            enemyController.ExitHitstun();
        }

        hitStunCoroutine = null;
    }

    private void PlayReactionAnimation(int comboStep)
    {

        switch (comboStep)
        {
            case 1:
                animator.SetTrigger("Hit1");
                break;
            case 2:
                animator.SetTrigger("Hit2");
                break;
            case 3:
                animator.SetTrigger("Hit3");
                break;
        }
    }
}
