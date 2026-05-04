using UnityEngine;

public class TreantStats : EnemyStats
{
    [SerializeField] private float moveSpeed = 4f;          // Player movespeed (UI display name: Swiftness)
    [SerializeField] private float Health = 4f;               // Max health (UI display name: Vitality)
    [SerializeField] private float attackPower = 2f;             // Player attack power (UI display name: Ferocity)
    [SerializeField] private float attackSpeed;             // Player attack speed (UI display name: Frenzy)
    [SerializeField] private float defense;                 // Player defense (UI display name: Fortitude)
    [SerializeField] private float recovery;                // Player health recovery (UI display name: Recovery)

    public override float MoveSpeed => moveSpeed;
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public float GetHealth()
    {
        return Health;
    }

    public float GetAttackPower()
    {
        return attackPower;
    }
}
