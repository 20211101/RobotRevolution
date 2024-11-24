using UnityEngine;
using System.Collections;
public class DroneBullet : MonoBehaviour
{
    MemoryPool pool;
    [SerializeField]
    float damage = 0;
    Rigidbody rigid;
    Vector3 dir = Vector3.zero;
    float speed = 20;
    public void Setup(MemoryPool pool, float damage, Vector3 dir)
    {
        if(rigid == null)
        rigid = GetComponent<Rigidbody>();

        this.pool = pool;
        this.damage = damage;
        this.dir = dir;

        rigid.linearVelocity = this.dir * speed;
        StartCoroutine(nameof(DestroyCount));
    }

    IEnumerator DestroyCount()
    {
        yield return new WaitForSeconds(3);
        
        pool.DeactivatePoolItem(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            StopAllCoroutines();
            other.GetComponent<EnemyBase>().Damaged(damage);
            pool.DeactivatePoolItem(gameObject);
        }

    }
}
