using UnityEngine;
using System.Collections;
using UnityEngine.AI;
public class SimpleEnemy : EnemyBase
{
    private SkinnedMeshRenderer[] render;
    private Color normalColor;
    [SerializeField]
    private Color damagedColor;
    [SerializeField]
    private Transform renderT;
    [SerializeField]
    Animator anim;

    private NavMeshAgent agent;  // NavMeshAgent ������Ʈ

    private Rigidbody rigidbody;
    private void Awake()
    {
        rigidbody = GetComponentInParent<Rigidbody>();
        render = GetComponentsInChildren<SkinnedMeshRenderer>();
        normalColor = render[0].material.color;
    }
    public override void Setup(MemoryPool pool, Transform target, float hp, float speed, float damage, float exp)
    {
        base.Setup(pool, target, hp, speed, damage, exp);
        agent = GetComponentInParent<NavMeshAgent>();  // ������Ʈ ��������
        agent.speed = speed;
        base.Setup(pool, target, hp, speed, damage, exp);
    }
    private void Update()
    {
        if (TimeCalculator.instance.leftT < 0)
            return;
        if (hp <= 0) return;
        Move();

        //        renderT.LookAt(target);
        renderT.LookAt(target);
    }
    public override void Move()
    {
        agent.destination = target.position;  // ��ǥ ���� ����
        Vector3 vel = (target.position - transform.position).normalized * speed;
        vel.y = 0;
        //rigidbody.linearVelocity = vel;
    }
    [SerializeField] GameObject parent;
    public override bool Damaged(float damage)
    {
        if (hp > 0 && damage >= hp)
        {
            hp = 0;
            anim.SetTrigger("Die");
            rigidbody.linearVelocity = Vector3.zero;
            StopCoroutine(nameof(DamagedEffect));
            Invoke("Die", 1f);
            foreach (SkinnedMeshRenderer mesh in render)
                    mesh.material.color = normalColor;
            PlayerBoby.instance.AddEXP(exp);
            return true;
        }
        else if (damage < hp)
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
