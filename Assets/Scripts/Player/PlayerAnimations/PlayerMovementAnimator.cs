using UnityEngine;
using UnityEngine.Windows;

public class PlayerMovementAnimator : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 input = playerMovement.GetMoveInput();
        Vector2 animDir;

        if (input != Vector2.zero)
        {
            animDir = GetDominantDirection(input);

            animator.SetFloat("LastMoveX", animDir.x);
            animator.SetFloat("LastMoveY", animDir .y);
        }
        else
        {
            // keep last direction when idle
            animDir = playerMovement.getFacingDirection();
        }

        animator.SetFloat("MoveX", animDir.x);
        animator.SetFloat("MoveY", animDir.y);
        animator.SetFloat("Speed", input.sqrMagnitude);

        // flip
        if (input.x < 0)
            spriteRenderer.flipX = true;
        else if (input.x > 0)
            spriteRenderer.flipX = false;
    }

    Vector2 GetDominantDirection(Vector2 direction)
    {
        if (direction == Vector2.zero)
            return Vector2.zero;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            return new Vector2(Mathf.Sign(direction.x), 0);
        }
        else
        {
            return new Vector2(0, Mathf.Sign(direction.y));
        }
    }
}
