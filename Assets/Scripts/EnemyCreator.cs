using UnityEngine;
using System.Collections;
public class EnemyCreator : MonoBehaviour
{
    [Header("���� �ӵ� + �д� �������� ����(ü�°� ���ط��� ������) ����ġ�� �߰� ����")]
    [SerializeField]
    float statMultiflyPerMin = 0.2f;
    [Header("�⺻ �� ����")]
    GameObject simpleEnemy;
    MemoryPool simpleEnemyPool;

    float finalMultifly => statMultiflyPerMin * TimeCalculator.instance.minute;

    private void Awake()
    {
    }
    EnemySpawnController head;
    public void Setup(EnemySpawnController head, GameObject simple, GameObject charge, GameObject range)
    {
        this.head = head;
        simpleEnemy = simple;
        chargeEnemy = charge;
        rangeEnemy = range;
        simpleEnemyPool = new MemoryPool(simpleEnemy);
        chargeEnemyPool = new MemoryPool(chargeEnemy);
        rangeEnemyEnemyPool = new MemoryPool(rangeEnemy);
    }

    public void SpawnSimpleEnemy()
    {
        GameObject obj = simpleEnemyPool.ActivatePoolItem(transform.position);
        EnemyBase enemy = obj.GetComponentInChildren<EnemyBase>();
        enemy.Setup(simpleEnemyPool, head.player,
            head.simpleEnemy_hp + head.simpleEnemy_hp * finalMultifly
            , head.simpleEnemy_speed,
            head.simpleEnemy_damage + head.simpleEnemy_damage * finalMultifly, head.simpleEnemy_exp + head.simpleEnemy_exp * statMultiflyPerMin);
    }
    [Header("�⺻ �� ����")]
    GameObject chargeEnemy;
    MemoryPool chargeEnemyPool;

    public void SpawnChargeEnemy()
    {
        GameObject obj = chargeEnemyPool.ActivatePoolItem(transform.position);
        EnemyBase enemy = obj.GetComponentInChildren<EnemyBase>();
        enemy.Setup(chargeEnemyPool, head.player, head.chargeEnemy_hp + head.chargeEnemy_hp * finalMultifly
            , head.chargeEnemy_speed, head.chargeEnemy_damage + head.chargeEnemy_damage * finalMultifly, head.chargeEnemy_exp + head.chargeEnemy_exp * statMultiflyPerMin);
    }
    [Header("�⺻ �� ����")]
    GameObject rangeEnemy;
    MemoryPool rangeEnemyEnemyPool;

    public void SpawnRangeEnemy()
    {
        GameObject obj = rangeEnemyEnemyPool.ActivatePoolItem(transform.position);
        EnemyBase enemy = obj.GetComponentInChildren<EnemyBase>();
        enemy.Setup(rangeEnemyEnemyPool, head.player, head.rangeEnemy_hp + head.rangeEnemy_hp * finalMultifly, head.rangeEnemyy_speed, head.rangeEnemy_damage + head.rangeEnemy_damage * finalMultifly, head.rangeEnemy_exp + head.rangeEnemy_exp * statMultiflyPerMin);
    }
}
