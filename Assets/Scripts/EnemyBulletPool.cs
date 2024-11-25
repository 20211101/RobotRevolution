using UnityEngine;

public class EnemyBulletPool : MonoBehaviour
{
    private static EnemyBulletPool _instance;
    public static EnemyBulletPool instance => _instance;

    [SerializeField]
    GameObject bullet;
    MemoryPool pool;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
        pool = new MemoryPool(bullet);
    }

    public MemoryPool BulletPool => pool;
}
