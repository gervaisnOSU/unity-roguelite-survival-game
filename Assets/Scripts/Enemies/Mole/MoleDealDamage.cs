using UnityEngine;

public class MoleDealDamage : MonoBehaviour
{
    private MoleStats moleStats;
    [SerializeField] private float damageCooldown = 1f;
    private float lastDamageTime;

    private void Start()
    {
        moleStats = GetComponent<MoleStats>();
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
                    player.TakeDamage(moleStats.GetAttackPower());
                    lastDamageTime = Time.time;
                }
            }
        }
    }
}
