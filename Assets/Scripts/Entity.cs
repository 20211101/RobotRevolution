using UnityEngine;

[System.Serializable]
public struct EntityStats
{
    [Header("Level, Exp")]
    public int level;
    public long exp;

    [Header("Attack")]
    public float cooldownTime;

    [Header("Defense")]
    public float currentHP;
    public float maxHP;
    public float evasion;
}

public abstract class Entity : MonoBehaviour
{
    [SerializeField]
    protected EntityStats stats;

    public EntityStats Stats => stats;
    public bool IsDead => stats.currentHP <= 0;

    public Entity Target { get; set; }

    protected virtual void Setup()
    {
        stats.currentHP = stats.maxHP;
    }

    public void TakeDamage(float damage)
    {
        if (IsDead) return;

        stats.currentHP = stats.currentHP - damage > 0 ? stats.currentHP - damage : 0;

        if (stats.currentHP <= 0)
        {
            Debug.Log("»ç¸Á!");
        }
    }
}
