using UnityEngine;

public class AtkCollider_DronePerpendicular : MonoBehaviour
{
    float damage = 0;
    public void Setup(float damage)
    {
        this.damage = damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            other.GetComponent<EnemyBase>().Damaged(damage);
    }
}
