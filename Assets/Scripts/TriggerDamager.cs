using UnityEngine;

public class TriggerDamager : MonoBehaviour
{
    // ! ��п��� �����ؼ� ������ �� �ٲ� !
    public int damage = 10;

    [SerializeField]
    string tagName;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(tagName))
        {
            other.GetComponent<Entity>().TakeDamage(damage);
        }
    }
}
