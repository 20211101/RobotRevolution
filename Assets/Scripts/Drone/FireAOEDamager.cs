using UnityEngine;

public class FireAOEDamager : MonoBehaviour
{
    float damage;
    public void Setup(float damage)
    {
        this.damage = damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyBase>().Damaged(damage);
        }
    }
}
