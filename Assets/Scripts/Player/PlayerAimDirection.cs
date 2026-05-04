using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimDirection : MonoBehaviour
{
    private Vector2 aimDirection;
    private Rigidbody2D playerRB;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get necessary player component
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get Player Position
        Vector2 playerPosition = playerRB.position;

        // Read the mouse position on screen
        Vector2 mousePositionScreen = Mouse.current.position.ReadValue();

        // Convert to the mouse position in world
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);

        // Convert mouse position world to a vector2
        Vector2 mousePositionWorld2D = new Vector2(mousePositionWorld.x,  mousePositionWorld.y);

        // Calculate the aim direction
        aimDirection = (mousePositionWorld2D - playerPosition).normalized;

    }

    public Vector2 GetAimDirection()
    {
        return aimDirection;
    }
}

