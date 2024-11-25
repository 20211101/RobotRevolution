using UnityEngine;

public enum EnemyType { Simple, Range, Charge}
public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField]
    protected Transform target;
    protected MemoryPool pool;
    protected float MAX_HP;
    protected float hp;
    protected float speed;
    protected float damage;
    protected float exp;

    public float HP => hp;
    public float MAXHP => MAX_HP;

    public virtual void Setup(MemoryPool pool, Transform target, float hp, float speed, float damage , float exp)
    {
        this.pool = pool;
        this.target = target;
        MAX_HP = hp;
        this.hp = hp;
        this.speed = speed;
        this.damage = damage;
        this.exp = exp;
    }
    public abstract bool Damaged(float damage);
    public abstract void Move();
}
