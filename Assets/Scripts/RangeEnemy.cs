using UnityEngine;
using System.Collections;
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

    private float atkRange = 4;
    private void Awake()
    {
        eRigidbody = GetComponentInParent<Rigidbody>();
        render = GetComponentsInChildren<SkinnedMeshRenderer>();
        anim = GetComponentInChildren<Animator>();
        normalColor = render[0].material.color;
    }

    private void Update()
    {
        renderT.LookAt(target);
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot_SingleShot_AR")) return;
        
        if ((target.position - transform.position).sqrMagnitude < atkRange * atkRange)
        {
            anim.SetTrigger("Attack");
            TriggerAttack();
        }
        else
            Move();
    }

    public void TriggerAttack()
    {
        eRigidbody.linearVelocity = Vector3.zero;
    }
    public void Attack()
    {
        GameObject bullet = EnemyBulletPool.instance.BulletPool.ActivatePoolItem(shootPos.position);
        Vector3 dir = (target.position - shootPos.position);
        dir.y = 0;
        bullet.GetComponent<EnemyBullet>().Setup(EnemyBulletPool.instance.BulletPool, damage, dir.normalized);
    }

    public override bool Damaged(float damage)
    {
        if (damage >= hp)
        {
            hp = 0;
            StopCoroutine(nameof(DamagedEffect));
            pool.DeactivatePoolItem(gameObject);
            foreach (SkinnedMeshRenderer mesh in render)
                mesh.material.color = normalColor;
            PlayerBoby.instance.AddEXP(exp);
            gameObject.SetActive(false);
            return true;
        }
        else
        {
            StopCoroutine(nameof(DamagedEffect));
            StartCoroutine(nameof(DamagedEffect));
            hp -= damage;
            return false;
        }
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
        eRigidbody.linearVelocity = vel;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerBoby>().TakeDamage(damage);
        }

    }
}
