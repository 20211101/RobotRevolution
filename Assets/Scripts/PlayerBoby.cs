using UnityEngine;
using System.Collections.Generic;
using System.Collections;
// 역할 :
// 행성 편입 관리
// 움직임 명령

public class UpgradeSource
{
    public int MaxUpgrade;
    public int CurUpgrade;
    public float increaseOffset;

    public bool CanUpgrade => CurUpgrade < MaxUpgrade;

    public UpgradeSource(int max, int cur, float inc)
    {
        MaxUpgrade = max;
        CurUpgrade = cur;
        increaseOffset = inc;
    }
}

public class PlayerBoby : MonoBehaviour
{
    private static PlayerBoby _instance;
    public static PlayerBoby instance
    {
        get => _instance;
    }

    [SerializeField]
    PlayerMovement movement;
    [SerializeField]
    PlayerRender render;
    [SerializeField]
    Transform head;
    [SerializeField]
    Rotator rotator;
    public int MAX_CHILD_CNT => rotator.MaxChildCnt;

    public List<DroneBase> drons = new List<DroneBase>();
    // 레이저 관련
    [SerializeField]
    float distance = 2;
    [SerializeField]
    float damage = 2;
    [SerializeField]
    float atkCoolTime = 0.5f;
    float curAtkCoolTime = 0;

    // 플레이어 업그레이드 정보(이속, 나사 양, 최대 드론 체력)
    public UpgradeSource SpeedUp        = new UpgradeSource(3, 0, 1);
    public UpgradeSource EXPUp          = new UpgradeSource(3, 0, 0);
    public UpgradeSource DroneCntUp     = new UpgradeSource(3, 0, 0);
    public UpgradeSource HealthUp       = new UpgradeSource(3, 0, 100);

    private float MAX_HP = 100;
    private float hp = 100;
    //Recovery per Scecond
    private float hpRecovery = 5;
    private bool isInvincibilityTime = false;
    private float invincibilityTime = 3f;
    private CapsuleCollider p_collider;
    public float MAXHP => MAX_HP;
    public float Hp => hp;

    public float MAX_EXP = 1000;
    public float curEXP = 0;
    public float expMultiply = 1;

    public void AddEXP(float exp)
    {
        curEXP += exp * expMultiply;
        if(curEXP > MAX_EXP)
        {
            curEXP = curEXP % MAX_EXP;
            MAX_EXP += 100;
            LevelUp();
        }
    }
    public void LevelUp()
    {
        
        UIController.instance.OpenUpgradeUI();
    }

    public void UpgradeSpeed()
    {
        SpeedUp.CurUpgrade++;
        movement.SpeedUp(SpeedUp.increaseOffset);
    }
    public void UpgradeEXP()
    {
        EXPUp.CurUpgrade++;
        expMultiply += EXPUp.increaseOffset;
    }
    public void UpgradeDroneCnt()
    {
        DroneCntUp.CurUpgrade++;
        rotator.UpCapacity();
    }
    public void UpgradeHealth()
    {
        HealthUp.CurUpgrade++;
        MAX_HP += HealthUp.increaseOffset;
        hp += HealthUp.increaseOffset;
    }
    public float Distance => distance;


    public bool CanAddDrone => drons.Count < rotator.MaxChildCnt;
    public void AddDrone(Transform t)
    {
        drons.Add(t.GetComponent<DroneBase>());
        rotator.AddChild(t);
    }


    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
        movement = GetComponent<PlayerMovement>();
        rotator = GetComponentInChildren<Rotator>();
        render = GetComponent<PlayerRender>();
        p_collider = GetComponent<CapsuleCollider>();
    }
    private void Update()
    {
        movement.Moving();
        PlayerThunder();
        AutoRecovery();
    }


    private void PlayerThunder()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(head.position, head.forward, out hit, Distance, (1 << 7)))
        {
            render.HitRender(hit);
            if(Time.time - curAtkCoolTime > atkCoolTime)
            {
                hit.collider.GetComponent<EnemyBase>().Damaged(damage);
                curAtkCoolTime = Time.time;
            }
        }
        else if (Physics.Raycast(head.position, head.forward, out hit, Distance, 1 << 6))
        {
            render.HitRender(hit);
            hit.collider.GetComponent<DestroyedDrone>().GetEnergy(this);
        }
        else
            render.NonHitRender();
    }

    public void TakeDamage(float damage)
    {
        if (isInvincibilityTime) return;
        StartCoroutine(nameof(SetInvincibillity));
        hp -= damage;
        if (hp < 0)
        {
            hp = 0;
            Debug.Log("게임 오버!");
        }
    }

    private void AutoRecovery()
    {
        float plusValue = Time.deltaTime * hpRecovery;
        hp = (hp + plusValue)  > MAX_HP ? MAX_HP : (hp + plusValue);
    }

    private IEnumerator SetInvincibillity()
    {
        float invincibillityCountDown = invincibilityTime;
        isInvincibilityTime = true;
        render.RenderInvincibillity();

        while(invincibillityCountDown > 0)
        {
            invincibillityCountDown -= Time.deltaTime;
            yield return null;
        }

        p_collider.enabled = false;
        yield return new WaitForSeconds(0.1f);
        p_collider.enabled = true;
        render.EndRenderInvincibillity();
        isInvincibilityTime = false;
    }
}
