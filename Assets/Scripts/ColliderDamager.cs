using UnityEngine;

public class ColliderDamager : MonoBehaviour
{
    // ! �θ� ������ �����ؼ� ������ �� �ٲ� !
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
