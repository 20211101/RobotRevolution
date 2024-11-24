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
        while(true)
        {
            yield return new WaitForSeconds(CreateSpeed);
            SpawnSimpleEnemy();
        }
    }

    public void SpawnSimpleEnemy()
    {
        GameObject obj = simpleEnemyPool.ActivatePoolItem(transform.position);
        SimpleEnemy enemy = obj.GetComponent<SimpleEnemy>();
        enemy.Setup(simpleEnemyPool, player, simpleEnemy_hp, simpleEnemy_speed, simpleEnemy_damage, simpleEnemy_exp);
    }
}
