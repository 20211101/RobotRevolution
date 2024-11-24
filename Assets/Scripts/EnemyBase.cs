using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField]
    protected Transform target;
    protected MemoryPool pool;
    protected float MAX_HP;
    protected float hp;
    protected float speed;
    protected float damage;

    public float HP => hp;
    public float MAXHP => MAX_HP;

    public virtual void Setup(MemoryPool pool, Transform target, float hp, float speed)
    {
        this.pool = pool;
        this.target = target;
        MAX_HP = hp;
        this.hp = hp;
        this.speed = speed;
    }
    public abstract bool Damaged(float damage);
    public abstract void Move();
}
