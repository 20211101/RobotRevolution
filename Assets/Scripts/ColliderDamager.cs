using UnityEngine;

public class ColliderDamager : MonoBehaviour
{
    // ! 부모 있으면 참조해서 강제로 값 바꿈 !
    public int damage = 10;

    [SerializeField]
    string tagName;


    private void OnCollisionEnter(Collision collision)
    {
        
        Debug.Log($"{collision.gameObject.name}");
        if (collision.gameObject.CompareTag(tagName))
        {
            collision.gameObject.GetComponent<Entity>().TakeDamage(damage);
        }
    }
}
