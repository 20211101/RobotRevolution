using UnityEngine;
using System.Collections;
using UnityEngine.AI;
public class RangeEnemy : EnemyBase
{
    private SkinnedMeshRenderer[] render;
    private Color normalColor;
    [SerializeField]
    private Color damagedColor;
    [SerializeField]
    private Transform renderT;
    [SerializeField]
    private Transform shootPos;

    private Rigidbody eRigidbody;
    private Animator anim;
    private NavMeshAgent navigation;

    public override void Setup(MemoryPool pool, Transform target, float hp, float speed, float damage, float exp)
    {
        base.Setup(pool, target, hp, speed, damage, exp);
        navigation.speed = speed;
    }

    [Header("공격사거리 초깃값")]
    private float atkRange = 4;
    private void Awake()
    {
        eRigidbody = GetComponentInParent<Rigidbody>();
        render = GetComponentsInChildren<SkinnedMeshRenderer>();
        anim = GetComponentInChildren<Animator>();
        normalColor = render[0].material.color;
        navigation = GetComponentInParent<NavMeshAgent>();
    }

    private void Update()
    {
        if (hp <= 0) return;
        renderT.LookAt(target);
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot_SingleShot_AR")) return;
        
        if ((target.position - transform.position).sqrMagnitude < atkRange * atkRange)
        {
            anim.SetTrigger("Attack");
            TriggerAttack();
        }
        else
        {
            navigation.speed = speed;
            Move();
        }
    }

    public void TriggerAttack()
    {
        navigation.speed = 0;
    }
    public void Attack()
    {
        GameObject bullet = EnemyBulletPool.instance.BulletPool.ActivatePoolItem(shootPos.position);
        Vector3 dir = (target.position - shootPos.position);
        dir.y = 0;
        bullet.GetComponent<EnemyBullet>().Setup(EnemyBulletPool.instance.BulletPool, damage, dir.normalized);
    }

    [SerializeField] GameObject parent;
    public override bool Damaged(float damage)
    {
        if (hp > 0 && damage >= hp)
        {
            anim.SetTrigger("Die");
            eRigidbody.linearVelocity = Vector3.zero;
            hp = 0;
            PlayerBoby.instance.AddEXP(exp);
            StopCoroutine(nameof(DamagedEffect));
            Invoke("Die", 1f);
            foreach (SkinnedMeshRenderer mesh in render)
                mesh.material.color = normalColor;
            return true;
        }
        else if(damage < hp)
        {
            StopCoroutine(nameof(DamagedEffect));
            StartCoroutine(nameof(DamagedEffect));
            hp -= damage;
        }
        return false;
    }
    public void Die()
    {
        pool.DeactivatePoolItem(parent);
    }
    private IEnumerator DamagedEffect()
    {
        foreach (SkinnedMeshRenderer mesh in render)
            mesh.material.color = damagedColor;
        yield return new WaitForSeconds(0.25f);
        foreach (SkinnedMeshRenderer mesh in render)
            mesh.material.color = normalColor;
    }
    public override void Move()
    {
        Vector3 vel = (target.position - transform.parent.position).normalized * speed;
        vel.y = 0;
        navigation.destination = target.position;
        //eRigidbody.linearVelocity = vel;

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerBoby>().TakeDamage(damage);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (hp > 0 && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerBoby>().TakeDamage(damage);
        }

    }
}
