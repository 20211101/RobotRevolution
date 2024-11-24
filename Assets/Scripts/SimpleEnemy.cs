using UnityEngine;
using System.Collections;
public class SimpleEnemy : EnemyBase
{
    private SkinnedMeshRenderer[] render;
    private Color normalColor;
    [SerializeField]
    private Color damagedColor;
    [SerializeField]
    private Transform renderT;

    private Rigidbody rigidbody;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        render = GetComponentsInChildren<SkinnedMeshRenderer>();
        normalColor = render[0].material.color;
    }
    public override void Setup(MemoryPool pool, Transform target, float hp, float speed, float damage, float exp)
    {
        base.Setup(pool,target, hp, speed, damage, exp);
    }
    private void Update()
    {
        Move();
        renderT.LookAt(target);
    }
    public override void Move()
    {
        
        Vector3 vel = (target.position - transform.position).normalized * speed;
        vel.y = 0;
        rigidbody.linearVelocity = vel;
    }
    public override bool Damaged(float damage)
    {
        if (damage >= hp)
        {
            hp = 0;
            StopCoroutine(nameof(DamagedEffect));
            pool.DeactivatePoolItem(gameObject);
            foreach(SkinnedMeshRenderer mesh in render)
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerBoby>().TakeDamage(damage);
        }

    }
}
