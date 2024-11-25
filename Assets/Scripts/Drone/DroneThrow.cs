using UnityEngine;
using System.Collections;
public class DroneThrow : DroneBase
{
    public bool amIMini = false;
    [SerializeField]
    float attackRate = 3f;
    float finalAttackRate;
    [SerializeField]
    float attackDamage = 2;
    float finalDamage = 2;
    [SerializeField]
    float attackDistance = 10;
    [SerializeField]
    Rotator rotator;
    [SerializeField]
    Transform droenRenderer;

    private IEnumerator Start()
    {
        rotator = GetComponent<Rotator>();
        finalAttackRate = attackRate;
        while (true)
        {
            Vector3 point = FindTarget();
            if (point != Vector3.zero)
            {
                ShootBullet(point);
                point.y = droenRenderer.position.y;
                droenRenderer.LookAt( point);
            }
            yield return new WaitForSeconds(finalAttackRate);
        }
    }

    public void AddMiniDrone()
    {
        if (ChildUpgrade <= childs.Count) return;
        if (amIMini == true) return;

        GameObject g = Instantiate(childPrefab);
        DroneThrow temp = g.GetComponentInChildren<DroneThrow>();

        temp.amIMini = true;
        temp.SetUpgrades(damageUpgrade, atkRateUpgrade);

        childs.Add(temp);
        rotator.AddChild(g.transform);
    }

    public override void Upgrade_Child()
    {
        if (!CanChildUp) return;
        rotator.UpCapacity();
        childUpgrade++;
        AddMiniDrone();
    }
    public override void Upgrade_Dmg()
    {
        if (!CanDamageUpgrade) return;
        damageUpgrade++;
        finalDamage = attackDamage + dmgUpOffset * damageUpgrade;

        foreach (DroneRange d in childs)
        {
            d.SetUpgrades(damageUpgrade, atkRateUpgrade);
        }
    }
    public override void Upgrade_AtkRate()
    {
        if (!CanAtkRateUpgrade) return;
        atkRateUpgrade++;
        finalAttackRate = attackRate - atkRateUpOffset * atkRateUpgrade;
        if (finalAttackRate <= 0)
            finalAttackRate = 0.1f;
        foreach (DroneRange d in childs)
        {
            d.SetUpgrades(damageUpgrade, atkRateUpgrade);
        }
    }
    public void SetUpgrades(int dmaUp, int ratUp)
    {
        damageUpgrade = dmaUp;

        atkRateUpgrade = ratUp;
        finalAttackRate = attackRate - atkRateUpOffset * atkRateUpgrade;
        if (finalAttackRate <= 0)
            finalAttackRate = 0.1f;
    }
    void OnDrawGizmos()
    {

        // Physics.SphereCast (레이저를 발사할 위치, 구의 반경, 발사 방향, 충돌 결과, 최대 거리)

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance / 2);
    }
    public Vector3 FindTarget()
    {
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(transform.position, attackDistance / 2, -Vector3.up, 10, LayerMask.GetMask("Enemy"));

        float closest = float.MaxValue;
        Vector3 pos = Vector3.zero;

        foreach (RaycastHit hit in hits)
        {
            Debug.Log(hit.collider.name);
            if (hit.distance < closest)
            {
                pos = hit.collider.transform.position;
            }
        }
        return pos;
    }
    public void ShootBullet(Vector3 pos)
    {
        GameObject bullet = FireAOEBulltPool.instance.BulletPool.ActivatePoolItem(transform.position);
        bullet.GetComponent<FireAOEBullt>().Setup(FireAOEBulltPool.instance,transform.position, pos, 5, finalDamage, 1);
    }
}
