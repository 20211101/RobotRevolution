using UnityEngine;
using System.Collections;
public class EnemySpawnController : MonoBehaviour
{

    [Header("스포너 정보")]
    [SerializeField]
    EnemyCreator[] creators;
    [SerializeField]
    EnemyCreator[] SpecialCreators;
    [Header("웨이브 세팅")]
    [SerializeField]
    EnemyType[] set1;
    [SerializeField]
    EnemyType[] set2;
    [SerializeField]
    EnemyType[] set3;
    [SerializeField]
    EnemyType[] set4;
    [SerializeField]
    EnemyType[] set5;
    [Header("생성 속도 + 분당 강해지는 배율(체력과 피해량이 증가함) 경험치도 추가 제공")]
    [SerializeField]
    public float statMultiflyPerMin = 0.2f;

    [Header("타깃")]
    [SerializeField]
    public Transform player;
    [Header("기본 적 스펙")]
    [SerializeField]
    GameObject simpleEnemy;
    [SerializeField]
    public float simpleEnemy_hp;
    [SerializeField]
    public float simpleEnemy_speed;
    [SerializeField]
    public float simpleEnemy_damage;
    [SerializeField]
    public float simpleEnemy_exp;
    [Header("돌진 적 스펙")]
    [SerializeField]
    GameObject chargeEnemy;
    [SerializeField]
    public float chargeEnemy_hp;
    [SerializeField]
    public float chargeEnemy_speed;
    [SerializeField]
    public float chargeEnemy_damage;
    [SerializeField]
    public float chargeEnemy_exp;
    [Header("원거리 적 스펙")]
    [SerializeField]
    GameObject rangeEnemy;
    [SerializeField]
    public float rangeEnemy_hp;
    [SerializeField]
    public float rangeEnemyy_speed;
    [SerializeField]
    public float rangeEnemy_damage;
    [SerializeField]
    public float rangeEnemy_exp;
    float DelaySec
    {
        get
        {
            int min = TimeCalculator.instance.minute;
            if (min < 1)
                return 5;
            else if (min < 3)
                return 4;
            else if (min < 5)
                return 5;
            else if (min < 7)
                return 4;
            else
                return 6;
        }
    }
    EnemyType[] Set
    {
        get
        {
            int min = TimeCalculator.instance.minute;
            if (min < 1)
                return set1;
            else if (min < 3)
                return set2;
            else if (min < 5)
                return set3;
            else if (min < 7)
                return set4;
            else
                return set5;
        }
    }
    float TimeScale = 0;
    public void Awake()
    {
        foreach (EnemyCreator t in creators)
            t.Setup(this, simpleEnemy, chargeEnemy, rangeEnemy);
        foreach (EnemyCreator t in SpecialCreators)
            t.Setup(this, simpleEnemy, chargeEnemy, rangeEnemy);
        
    }
    IEnumerator Start()
    {
        while (true)
        {
            foreach (EnemyType t in Set)
            {
                EnemyCreator position = creators[Random.Range(0, creators.Length)];
                switch (t)
                {
                    case EnemyType.Simple:
                        position.SpawnSimpleEnemy();
                        break;
                    case EnemyType.Range:
                        position.SpawnRangeEnemy();
                        break;
                    case EnemyType.Charge:
                        position.SpawnChargeEnemy();
                        break;
                }
            }
            yield return new WaitForSeconds(DelaySec);
            TimeScale += DelaySec;
            if (TimeScale > 60)
            {
                foreach (EnemyCreator t in SpecialCreators)
                {
                    t.SpawnSimpleEnemy();
                }
                TimeScale %= 60;
            }
        }
    }
}