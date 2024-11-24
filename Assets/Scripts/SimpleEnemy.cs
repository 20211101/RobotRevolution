using UnityEngine;
using System.Collections;
public class SimpleEnemy : EnemyBase
{
    private MeshRenderer render;
    private Color normalColor;
    [SerializeField]
    private Color damagedColor;

    private Rigidbody rigidbody;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        render = GetComponent<MeshRenderer>();
        normalColor = render.material.color;
    }
    public override void Setup(MemoryPool pool, Transform target, float hp, float speed)
    {
        base.Setup(pool,target, hp, speed);
    }
    private void Update()
    {
        Move();
    }
    public override void Move()
    {
        
        Vector3 vel = (target.position - transform.position).normalized * speed;
        vel.y = 0;
        rigidbody.linearVelocity = vel;
    }
    public override bool Damaged(float damage)
    {
        StopCoroutine(nameof(DamagedEffect));
        StartCoroutine(nameof(DamagedEffect));
        Debug.Log("MAX : " + MAX_HP);
        Debug.Log(hp);
        if (damage >= hp)
        {
            hp = 0;
            StopCoroutine(nameof(DamagedEffect));
            pool.DeactivatePoolItem(gameObject);
            render.material.color = normalColor;
            gameObject.SetActive(false);
            return true;
        }
        else
        {
            hp -= damage;
            return false;
        }
    }

    private IEnumerator DamagedEffect()
    {
        render.material.color = damagedColor;
        yield return new WaitForSeconds(0.25f);
        render.material.color = normalColor;
    }
}
