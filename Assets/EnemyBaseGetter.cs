using UnityEngine;

public class EnemyBaseGetter : MonoBehaviour
{
    public EnemyBase e_base;
    void Awake()
    {
        e_base = GetComponentInChildren<EnemyBase>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
