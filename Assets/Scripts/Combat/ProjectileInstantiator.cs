using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileInstantiator : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform spawnPoint;
    public float launchForce = 30f;
    public PlayerAimDirection playerAimDirection;
    public InputAction clickAction;
    [SerializeField] private PlayerStats playerStats;

    private void OnEnable()
    {
        // Enable clickAction
        clickAction.Enable();
    }

    private void OnDisable()
    {
        clickAction.Disable();
    }

    void Awake()
    {
        clickAction.performed += ctx => fireProjectile();
    }

    private void fireProjectile()
    {
        if (GameManager.Instance != null && !GameManager.Instance.IsGamePlaying())
        {
            return;
        }

        // Store aim direction
        Vector2 aimDirection = playerAimDirection.GetAimDirection();

        // Convert vector to an angle
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg + 270;

        // Instantiate the projectile
        GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);

        ArrowDamage arrowDamage = projectile.GetComponent<ArrowDamage>();
        if (arrowDamage != null)
        {
            arrowDamage.SetDamage(playerStats.GetAttackPower());
        }

        // Rotate the projectile to face the correct direction
        projectile.transform.rotation = Quaternion.Euler(0,0,angle);

        Rigidbody2D body = projectile.GetComponent<Rigidbody2D>();

        if (body != null)
        {
            // Apply force in the direction of the mouse
            body.AddForce(aimDirection * launchForce, ForceMode2D.Impulse);
        } 
    }
}
