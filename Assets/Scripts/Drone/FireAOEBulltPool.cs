using UnityEngine;

public class FireAOEBulltPool : MonoBehaviour
{
    private static FireAOEBulltPool _instance;
    public static FireAOEBulltPool instance => _instance;

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
