using UnityEngine;
using System.Collections.Generic;
// ���� :
// �༺ ���� ����
// ������ ���
public class PlayerBoby : MonoBehaviour
{
    [SerializeField]
    PlayerMovement movement;
    [SerializeField]
    PlayerRender render;
    [SerializeField]
    Transform head;
    [SerializeField]
    Rotator rotator;
    public List<DroneBase> drons = new List<DroneBase>();
    // ������ ����
    [SerializeField]
    float distance = 2;
    [SerializeField]
    float damage = 2;
    [SerializeField]
    float atkCoolTime = 0.5f;
    float curAtkCoolTime = 0;
    public float Distance => distance;
    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        rotator = GetComponentInChildren<Rotator>();
        render = GetComponent<PlayerRender>();
    }

    public void AddDrone(Transform t)
    {
        drons.Add(t.GetComponent<DroneBase>());
        rotator.AddChild(t);
    }

    private void Update()
    {
        movement.Moving();
        PlayerThunder();
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            drons[0].Upgrade_Child();
        }
    }

    private void UpgradeDrone()
    {

    }

    private void PlayerThunder()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(head.position, head.forward, out hit, Distance, 1 << 7))
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
            Debug.Log("������");
            render.HitRender(hit);
            hit.collider.GetComponent<DestroyedDrone>().GetEnergy(this);
        }
        else
            render.NonHitRender();
    }
}
