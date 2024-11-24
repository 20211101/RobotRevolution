using UnityEngine;
using System.Collections;
public class FireAOE : MonoBehaviour
{
    [SerializeField]
    MeshCollider m_collider;
    float duration = 1;
    float delay = 0.2f;
    float damage = 0.2f;
    float size = 5;
    public void Setup(float duration, float damage, float size)
    {
        StartCoroutine(nameof(Fire));
        transform.localScale = new Vector3(size, 0.05f, size);
        Destroy(gameObject, duration);
    }

    IEnumerator Fire()
    {
        while(true)
        {
            m_collider.enabled = true;
            yield return new WaitForSeconds(0.1f);
            m_collider.enabled = false;
            yield return new WaitForSeconds(delay);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyBase>().Damaged(damage);
        }
    }
}
