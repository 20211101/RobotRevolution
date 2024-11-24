using UnityEngine;
using System.Collections;
public class DronePerpendicular : DroneBase
{
    public bool amIMini = false;
    [SerializeField]
    float attackRate = 1f;
    float finalAttackRate ;
    [SerializeField]
    float attackDamage = 10;
    [SerializeField]
    GameObject atkCollider;
    AtkCollider_DronePerpendicular aCollider;
    Rotator rotator;

    private IEnumerator Start()
    {
        aCollider = atkCollider.GetComponentInChildren<AtkCollider_DronePerpendicular>();
        aCollider.Setup(attackDamage);
        rotator = GetComponent<Rotator>();
        finalAttackRate = attackRate;
        while(true)
        {
            atkCollider.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            atkCollider.SetActive(false);
            yield return new WaitForSeconds(attackRate);
        }
    }
    
    public void AddMiniDrone()
    {
        if (ChildUpgrade <= childs.Count) return;
        if (amIMini == true) return;
        GameObject g = Instantiate(childPrefab);
        DronePerpendicular temp = g.GetComponent<DronePerpendicular>();
        temp.amIMini = true;
        temp.SetUpgrades(damageUpgrade, atkRateUpgrade);
        childs.Add(temp);
        rotator.AddChild(g.transform);
    }

    public override void Upgrade_Child()
    {
        if (!CanChildUp) return;
        childUpgrade++;
        rotator.UpCapacity();
        AddMiniDrone();
    }
    public override void Upgrade_Dmg()
    {
        if (!CanDamageUpgrade) return;
        damageUpgrade++;
        aCollider.Setup(attackDamage + dmgUpOffset * damageUpgrade);
        foreach(DronePerpendicular d in childs)
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
        foreach (DronePerpendicular d in childs)
        {
            d.SetUpgrades(damageUpgrade, atkRateUpgrade);
        }
    }
    public void SetUpgrades(int dmaUp, int ratUp)
    {
        damageUpgrade = dmaUp;
        if(aCollider == null)
            aCollider = atkCollider.GetComponentInChildren<AtkCollider_DronePerpendicular>();

        aCollider.Setup(attackDamage + dmgUpOffset * damageUpgrade);

        atkRateUpgrade = ratUp;
        finalAttackRate = attackRate - atkRateUpOffset * atkRateUpgrade;
        if (finalAttackRate <= 0)
            finalAttackRate = 0.1f;
    }
}
