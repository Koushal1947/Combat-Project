using System;
using Unity.VisualScripting;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] private BoxCollider hitboxCollider;
    [SerializeField] private PlayerCombat playerCombat;
    [SerializeField] private AttackController attackController;
    [SerializeField] private EnemyReaction enemyReaction;
    [SerializeField] private HitStopController hitstopController;

     

    private void Awake()
    {
        hitboxCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        int damage = playerCombat.GetCurrentDamage();
        float hitstun = playerCombat.GetCurrentHitstun();
        float knockback = playerCombat.GetCurrentKnockback();
        float hitstop = playerCombat.GetCurrentHitstop();
        int comboStep = playerCombat.GetCurrentComboStep();
        bool causesKnockdown = playerCombat.CurrentAttackCausesKnockdown;
        HitReactionType reactionType = playerCombat.GetCurrentReactionType();


        if (!other.CompareTag("Enemy"))
        {
            return;
        }

        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

        if(enemyHealth == null)
        {
            return;
        }

        if (!attackController.TryRegisterHit(enemyHealth))
        {
            return;
        }
        

        enemyHealth.TakeDamage(damage);


        if(enemyReaction != null)
        {
            enemyReaction.PlayHitReaction(hitstun, knockback, transform.root, comboStep, causesKnockdown, reactionType);
        }

        hitstopController.PlayHitstop(hitstop);

    }
    public void EnableHitbox()
    {
        hitboxCollider.enabled = true;
    }

    public void DisableHitbox()
    {
        hitboxCollider.enabled = false;
    }

    

}
