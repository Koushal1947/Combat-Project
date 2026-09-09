using UnityEngine;


public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyReaction enemyReaction;
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        Debug.Log("Enemy Health: " + currentHealth);

       

        if(currentHealth <= 0)
        {
            Die();
            return;
        }

        enemyReaction.PlayHitReaction();
    }

    

    void Die()
    {
        Debug.Log("Enemy Died");
    }

}
