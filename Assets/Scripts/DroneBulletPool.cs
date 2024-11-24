using UnityEngine;

public class DroneBulletPool : MonoBehaviour
{
    private static DroneBulletPool _instance;
    public static DroneBulletPool instance => _instance;

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
