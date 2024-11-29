using UnityEngine;
using System.Collections;
using UnityEngine.AI;
public class ChargeEnemy : EnemyBase
{
    private SkinnedMeshRenderer[] render;
    private Color normalColor;
    [SerializeField]
    private Color damagedColor;
    [SerializeField]
    private Transform renderT;
    [SerializeField]
    private GameObject atkAreaRenderer;

    private Rigidbody eRigidbody;
    private NavMeshAgent agent;  // NavMeshAgent 컴포넌트
    private Animator anim;

    private float atkRange = 2;
    private void Awake()
    {
        eRigidbody = GetComponentInParent<Rigidbody>();
        render = GetComponentsInChildren<SkinnedMeshRenderer>();
        agent = GetComponentInParent<NavMeshAgent>();  // 컴포넌트 가져오기
        anim = GetComponentInChildren<Animator>();
        normalColor = render[0].material.color;
    }

    private void Update()
    {
        if (hp <= 0) return;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("H2H_PowerPunch01_Forward"))
        {
            Debug.Log(anim.GetCurrentAnimatorStateInfo(0).ToString());
            return; }
        renderT.LookAt(target);
        if ( (target.position - transform.position).sqrMagnitude < atkRange * atkRange)
        {
            anim.SetTrigger("Attack");
            TriggerAttack();
            eRigidbody.linearVelocity = Vector3.zero;
        }
        else
        {
            agent.speed = speed;
            Move();
        }
    }

    public void TriggerAttack()
    {
        agent.speed = 0;
        atkAreaRenderer.SetActive(true);
    }
    public void DisableAtkArea()
    {
        atkAreaRenderer.SetActive(false);
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 0.5f, transform.forward, 5.5f, (1 << 8));
        foreach (RaycastHit hit in hits)
        {
            hit.collider.gameObject.GetComponent<PlayerBoby>().TakeDamage(damage);
        }

    }    
    public void Attack()
    {
        transform.parent.position = transform.position + transform.forward * 5.5f;
    }

    [SerializeField] GameObject parent;
    public override bool Damaged(float damage)
    {
        if (hp > 0 && damage >= hp)
        {
            hp = 0;
            anim.SetTrigger("Die");
            eRigidbody.linearVelocity = Vector3.zero;
            atkAreaRenderer.SetActive(false);
            Invoke("Die", 1f);
            StopCoroutine(nameof(DamagedEffect));
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
    public override void Move()
    {
        Vector3 vel = (target.position - transform.parent.position).normalized * speed;
        vel.y = 0;
        //eRigidbody.linearVelocity = vel;
        agent.destination = target.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerBoby>().TakeDamage(damage);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (hp > 0 && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerBoby>().TakeDamage(damage);
        }

    }
}
