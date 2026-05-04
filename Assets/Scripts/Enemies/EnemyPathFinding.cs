using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    private EnemyStats enemyStats;
    private Rigidbody2D enemyRB;
    private Rigidbody2D playerRB;
    private float moveSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Get stats and rigidbodies
        enemyStats = GetComponent<EnemyStats>();
        enemyRB = GetComponent<Rigidbody2D>();
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        moveSpeed = enemyStats.MoveSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.Instance != null && !GameManager.Instance.IsGamePlaying())
        {
            return;
        }

        Vector2 newPostion = Vector2.MoveTowards(enemyRB.position, playerRB.position, moveSpeed * 0.8f * Time.fixedDeltaTime);
        enemyRB.MovePosition(newPostion);
    }
}
