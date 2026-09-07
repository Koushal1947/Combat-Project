using UnityEngine;
using Unity.Collections;
using System.Collections;

public class EnemyReaction : MonoBehaviour
{
    private bool isHitStun;

    public bool isInHitStun => isHitStun;
    [SerializeField] private float hitStunTime = 0.25f;
    public void PlayHitReaction()
    {
        StartCoroutine(HitStun());
    }

    private IEnumerator HitStun()
    {
        isHitStun = true;

        //hit animation
        Debug.Log("Enemy stunned");

        yield return new WaitForSeconds(hitStunTime);

        isHitStun = false;
        Debug.Log("Stun Recovered");
    }
}
