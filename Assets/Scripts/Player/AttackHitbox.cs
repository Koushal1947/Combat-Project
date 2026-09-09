using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] private BoxCollider hitboxCollider;
    [SerializeField] private int damage = 20;

    private void Awake()
    {
        hitboxCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Hit!");
        }

        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

        if(enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }
        
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
