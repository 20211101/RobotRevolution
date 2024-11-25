using UnityEngine;
using System.Collections;
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
    private Animator anim;

    private float atkRange = 2;
    private void Awake()
    {
        eRigidbody = GetComponentInParent<Rigidbody>();
        render = GetComponentsInChildren<SkinnedMeshRenderer>();
        anim = GetComponentInChildren<Animator>();
        normalColor = render[0].material.color;
    }

    private void Update()
    {

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("H2H_PowerPunch01_Forward")) return;
        renderT.LookAt(target);
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
