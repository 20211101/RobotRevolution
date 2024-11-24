using UnityEngine;
using System.Collections;
public class FireAOE : MonoBehaviour
{
    [SerializeField]
    MeshCollider m_collider;
    [SerializeField]
    FireAOEDamager m_damager;
    float duration = 5;
    float delay = 0.2f;
    float size = 1;
    public void Setup(float duration, float damage, float size)
    {
        m_damager.Setup(damage);
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

}
