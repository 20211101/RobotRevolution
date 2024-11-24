using UnityEngine;
using System.Collections;
public class EnemyCreator : MonoBehaviour
{
    [SerializeField]
    GameObject simpleEnemy;
    [SerializeField]
    float simpleEnemy_hp;
    [SerializeField]
    float simpleEnemy_speed;
    [SerializeField]
    Transform player;
    MemoryPool simpleEnemyPool;

    private IEnumerator Start()
    {
        simpleEnemyPool = new MemoryPool(simpleEnemy);
        while(true)
        {
            yield return new WaitForSeconds(10);
            SpawnSimpleEnemy();
        }
    }

    public void SpawnSimpleEnemy()
    {
        GameObject obj = simpleEnemyPool.ActivatePoolItem(transform.position);
        SimpleEnemy enemy = obj.GetComponent<SimpleEnemy>();
        enemy.Setup(simpleEnemyPool, player, simpleEnemy_hp, simpleEnemy_speed);
    }
}
