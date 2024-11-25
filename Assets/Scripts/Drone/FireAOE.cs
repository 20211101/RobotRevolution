using UnityEngine;
using System.Collections;
public enum AOET { bullet, barrel}
public class FireAOE : MonoBehaviour
{
    [SerializeField]
    AOET t = AOET.bullet;
    [SerializeField]
    MeshCollider m_collider;
    [SerializeField]
    FireAOEDamager m_damager;
    float damage;
    float duration = 5;
    float delay = 0.2f;
    float size = 1;
    public void Setup(float duration, float damage, float size)
    {
        this.damage = damage;
        m_damager.Setup(damage);
        m_damager.gameObject.SetActive(false);
        StartCoroutine(nameof(Fire));
        //transform.localScale = new Vector3(size, 1, size);
        Destroy(gameObject, duration);
    }

    IEnumerator Fire()
    {
        while(true)
        {
            RaycastHit[] hits;
            if (t == AOET.bullet)
                hits = Physics.SphereCastAll(transform.position, 2, Vector3.up, 5f,(1 << 7));
            else
                hits = Physics.SphereCastAll(transform.position - new Vector3(0,2,0), 5, Vector3.up, 20f, (1 << 7));
            foreach (RaycastHit hit in hits)
            {
                Debug.Log(hits.Length);

                EnemyBase b = hit.collider.GetComponent<EnemyBase>();
                if(b != null)
                    b.Damaged(damage);
            }
            yield return new WaitForSeconds(delay);
        }
    }

}
