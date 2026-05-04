using UnityEngine;

public class ArrowDamage : MonoBehaviour
{
    private float damage = 1f;

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    public float GetDamage()
    {
        return damage;
    }
}