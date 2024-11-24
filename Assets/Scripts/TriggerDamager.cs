using UnityEngine;

public class TriggerDamager : MonoBehaviour
{
    // ! 드론에서 참조해서 강제로 값 바꿈 !
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
