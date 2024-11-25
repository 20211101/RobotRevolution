using UnityEngine;
using System.Collections;
public class ChargeEnemyCreator : MonoBehaviour
{
    [SerializeField]
    float CreateSpeed = 5f;
    [Header("±âº» Àû ½ºÆå")]
    [SerializeField]
    GameObject chargeEnemy;
    [SerializeField]
    float chargeEnemy_hp;
    [SerializeField]
    float chargeEnemy_speed;
    [SerializeField]
    float chargeEnemy_damage;
    [SerializeField]
    float chargeEnemy_exp;
    [SerializeField]
    Transform player;
    MemoryPool chargeEnemyPool;

    private IEnumerator Start()
    {
        chargeEnemyPool = new MemoryPool(chargeEnemy);
        while (true)
        {
            yield return new WaitForSeconds(CreateSpeed);
            SpawnChargeEnemy();
        }
    }

    public void SpawnChargeEnemy()
    {
        GameObject obj = chargeEnemyPool.ActivatePoolItem(transform.position);
        EnemyBase enemy = obj.GetComponent<EnemyBase>();
        enemy.Setup(chargeEnemyPool, player, chargeEnemy_hp, chargeEnemy_speed, chargeEnemy_damage, chargeEnemy_exp);
    }
}
