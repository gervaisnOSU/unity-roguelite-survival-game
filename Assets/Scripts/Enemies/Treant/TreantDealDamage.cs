using Unity.VisualScripting;
using UnityEngine;

public class TreantDealDamage : MonoBehaviour
{
    private TreantStats treantStats;
    [SerializeField] private float damageCooldown = 1f;
    private float lastDamageTime;

    private void Start()
    {
        treantStats = GetComponent<TreantStats>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Time.time >= lastDamageTime + damageCooldown)
            {
                PlayerStats player = collision.GetComponent<PlayerStats>();

                if (player != null)
                {
                    player.TakeDamage(treantStats.GetAttackPower());
                    lastDamageTime = Time.time;
                }
            }
        }
    }
}
