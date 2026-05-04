using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;         // Player movespeed (UI display name: Swiftness)
    [SerializeField] private float maxHealth = 5f;               // Max health (UI display name: Vitality)
    [SerializeField] private float currentHealth;           // Current runtime health
    [SerializeField] private float attackPower = 1f;             // Player attack power (UI display name: Ferocity)
    [SerializeField] private float attackSpeed;             // Player attack speed (UI display name: Frenzy)
    [SerializeField] private float defense;                 // Player defense (UI display name: Fortitude)
    [SerializeField] private float recovery;                // Player health recovery (UI display name: Recovery)
    private float lastHitTime;
    private Coroutine flashRoutine;
    [SerializeField] private float invincibleTime = 0.5f;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private Color flashColor = Color.red;
    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public float GetAttackPower()
    {
        return attackPower;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
        {
            Debug.Log("Damage ignored: player is dead.");
            return;
        }

        if (Time.time < lastHitTime + invincibleTime)
        {
            Debug.Log("Damage ignored: invincibility active.");
            return;
        }

        currentHealth = Mathf.Max(currentHealth - damage, 0f);
        lastHitTime = Time.time;

        Debug.Log("Player took damage: " + damage + ". Current health: " + currentHealth);

        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(DamageFlash());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private System.Collections.IEnumerator DamageFlash()
    {
        float flashes = 2f;
        float interval = flashDuration / (flashes * 2f);

        for (int i = 0; i < flashes; i++)
        {
            spriteRenderer.color = flashColor;
            yield return new WaitForSeconds(interval);

            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(interval);
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Player has died.");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoseGame();
        }
    }
}
