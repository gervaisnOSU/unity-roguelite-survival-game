using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Declare references to required player components
    private PlayerStats playerStats;
    private Rigidbody2D playerRB;
    private Vector2 moveInput;
    private Vector2 facingDirection;

    void Start()
    {
        // Get necessary player components
        playerStats = GetComponent<PlayerStats>();
        playerRB = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue value)
    {
        if (GameManager.Instance != null && !GameManager.Instance.IsGamePlaying())
        {
            moveInput = Vector2.zero;
            return;
        }

        moveInput = value.Get<Vector2>(); // Store the current input movement

        // Update the player facing direction every time the player is moving
        if (moveInput != Vector2.zero)
        {
            facingDirection = moveInput;
        }
    }

    void FixedUpdate()
    {
        if (GameManager.Instance != null && !GameManager.Instance.IsGamePlaying())
        {
            moveInput = Vector2.zero;
            playerRB.linearVelocity = Vector2.zero;
            return;
        }

        float moveSpeed = playerStats.GetMoveSpeed(); // Read the current move speed from PlayerStats
        playerRB.MovePosition(playerRB.position + moveInput * moveSpeed * Time.fixedDeltaTime); // Move the rigid body based on input
    }

    public Vector2 getFacingDirection()
    {
        return facingDirection;
    }

    public Vector2 GetMoveInput()
    {
        return moveInput;
    }
}