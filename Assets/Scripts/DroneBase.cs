using UnityEngine;
using System.Collections.Generic;

public enum DroneT { perpendicular, range, throwgh}
public abstract class DroneBase : MonoBehaviour
{
    public DroneT type;

    protected int maxChildUpgrade = 3;
    protected int childUpgrade = 0;
    public bool CanChildUp => childUpgrade < maxChildUpgrade;
    [SerializeField]
    protected GameObject childPrefab;
    protected List<DroneBase> childs = new List<DroneBase>();

    protected int maxDamageUpgrade = 3;
    protected int damageUpgrade = 0;
    public bool CanDamageUpgrade => damageUpgrade < maxDamageUpgrade;
    [SerializeField]
    protected float dmgUpOffset = 0;

    protected int maxAtkRateUpgrade = 3;
    protected int atkRateUpgrade = 0;
    public bool CanAtkRateUpgrade => atkRateUpgrade < maxAtkRateUpgrade;
    [SerializeField]
    protected float atkRateUpOffset = 0;

    public int MaxChildUpgrade => maxChildUpgrade;
    public int ChildUpgrade => childUpgrade;
    public int MaxDamageUpgrade => maxDamageUpgrade;
    public int DamageUpgrade => damageUpgrade;
    public int MaxAtkRateUpgrade => maxAtkRateUpgrade;
    public int AtkRateUpgrade => atkRateUpgrade;

    public abstract void Upgrade_Child();
    public abstract void Upgrade_Dmg();
    public abstract void Upgrade_AtkRate();
}
