using UnityEngine;
using System.Collections;
public class EnemyCreator : MonoBehaviour
{
    [SerializeField]
    float  CreateSpeed = 5f;
    [Header("±âº» Àû ½ºÆå")]
    [SerializeField]
    GameObject simpleEnemy;
    [SerializeField]
    float simpleEnemy_hp;
    [SerializeField]
    float simpleEnemy_speed;
    [SerializeField]
    float simpleEnemy_damage;
    [SerializeField]
    float simpleEnemy_exp;
    [SerializeField]
    Transform player;
    MemoryPool simpleEnemyPool;

    private IEnumerator Start()
    {
        simpleEnemyPool = new MemoryPool(simpleEnemy);
        chargeEnemyPool = new MemoryPool(chargeEnemy);
        rangeEnemyEnemyPool = new MemoryPool(rangeEnemy);
        while (true)
        {
            yield return new WaitForSeconds(CreateSpeed);
            SpawnChargeEnemy();
            SpawnSimpleEnemy();
            SpawnRangeEnemy();
        }
    }

    public void SpawnSimpleEnemy()
    {
        GameObject obj = simpleEnemyPool.ActivatePoolItem(transform.position);
        EnemyBase enemy = obj.GetComponentInChildren<EnemyBase>();
        enemy.Setup(simpleEnemyPool, player, simpleEnemy_hp, simpleEnemy_speed, simpleEnemy_damage, simpleEnemy_exp);
    }
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
    MemoryPool chargeEnemyPool;

    public void SpawnChargeEnemy()
    {
        GameObject obj = chargeEnemyPool.ActivatePoolItem(transform.position);
        EnemyBase enemy = obj.GetComponentInChildren<EnemyBase>();
        enemy.Setup(chargeEnemyPool, player, chargeEnemy_hp, chargeEnemy_speed, chargeEnemy_damage, chargeEnemy_exp);
    }
    [Header("±âº» Àû ½ºÆå")]
    [SerializeField]
    GameObject rangeEnemy;
    [SerializeField]
    float rangeEnemy_hp;
    [SerializeField]
    float rangeEnemyy_speed;
    [SerializeField]
    float rangeEnemy_damage;
    [SerializeField]
    float rangeEnemy_exp;
    MemoryPool rangeEnemyEnemyPool;

    public void SpawnRangeEnemy()
    {
        GameObject obj = rangeEnemyEnemyPool.ActivatePoolItem(transform.position);
        EnemyBase enemy = obj.GetComponentInChildren<EnemyBase>();
        enemy.Setup(rangeEnemyEnemyPool, player, rangeEnemy_hp, rangeEnemyy_speed, rangeEnemy_damage, rangeEnemy_exp);
    }
}
