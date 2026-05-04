using UnityEngine;

public class MoleTakeDamageAndDie : MonoBehaviour
{
    private MoleStats moleStats;
    private float health;
    private bool isDead = false;

    [Header("Hit Feedback")]
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private float flashDuration = 0.1f;

    [Header("Knockback")]
    [SerializeField] private float knockbackDistance = 0.4f;
    [SerializeField] private float knockbackDuration = 0.08f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Rigidbody2D enemyRB;
    private EnemyPathFinding enemyPathFinding;

    private Coroutine flashRoutine;
    private Coroutine knockbackRoutine;

    void Start()
    {
        moleStats = GetComponent<MoleStats>();
        health = moleStats.GetHealth();

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        else
        {
            Debug.LogError("No SpriteRenderer found on enemy or its children.");
        }

        enemyRB = GetComponent<Rigidbody2D>();
        enemyPathFinding = GetComponent<EnemyPathFinding>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead)
        {
            return;
        }

        if (collision.CompareTag("Arrow"))
        {
            ArrowDamage arrow = collision.GetComponentInParent<ArrowDamage>();

            if (arrow == null)
            {
                Debug.LogError("ArrowDamage NOT FOUND");
                return;
            }

            float damage = arrow.GetDamage();
            health -= damage;

            // Hit flash
            if (flashRoutine != null)
            {
                StopCoroutine(flashRoutine);
            }

            flashRoutine = StartCoroutine(DamageFlash());

            // Knockback
            Vector2 knockbackDirection = ((Vector2)transform.position - (Vector2)collision.transform.position).normalized;

            if (knockbackRoutine != null)
            {
                StopCoroutine(knockbackRoutine);
            }

            knockbackRoutine = StartCoroutine(Knockback(knockbackDirection));

            if (health <= 0)
            {
                Die();
            }
        }
    }

    private System.Collections.IEnumerator DamageFlash()
    {
        if (spriteRenderer == null)
        {
            Debug.LogError("Cannot flash because spriteRenderer is null.");
            yield break;
        }

        //Debug.Log("Flash started. Current color: " + spriteRenderer.color);

        spriteRenderer.color = flashColor;
        //Debug.Log("Changed enemy color to: " + spriteRenderer.color);

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.color = originalColor;
        //Debug.Log("Flash ended. Restored color to: " + spriteRenderer.color);
    }

    private System.Collections.IEnumerator Knockback(Vector2 direction)
    {
        if (enemyPathFinding != null)
        {
            enemyPathFinding.enabled = false;
        }

        float elapsed = 0f;
        Vector2 startPosition = enemyRB.position;
        Vector2 targetPosition = startPosition + direction * knockbackDistance;

        while (elapsed < knockbackDuration)
        {
            elapsed += Time.deltaTime;

            Vector2 newPosition = Vector2.Lerp(startPosition, targetPosition, elapsed / knockbackDuration);
            enemyRB.MovePosition(newPosition);

            yield return null;
        }

        if (!isDead && enemyPathFinding != null)
        {
            enemyPathFinding.enabled = true;
        }
    }

    private void Die()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;
        Debug.Log("Enemy died");

        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();

        foreach (Collider2D col in colliders)
        {
            col.enabled = false;
        }

        if (enemyPathFinding != null)
        {
            enemyPathFinding.enabled = false;
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.EnemyDefeated();
        }

        Destroy(gameObject);
    }
}