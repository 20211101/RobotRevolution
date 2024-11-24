using UnityEngine;

public class PlayerRender : MonoBehaviour
{
    // �� �������� �ִ�
    [SerializeField]
    Transform playerSphere;
    PlayerMovement movement;
    PlayerBoby playerBoby;
    [SerializeField]
    float rotateWeight = 10;
    // Ÿ�� �ٶ󺸴� �ִ�
    [SerializeField]
    Transform head;
    [SerializeField]
    GameObject temp;
    Transform target;
    [SerializeField]
    Transform electricEndPoint;
    // �÷��̾� ���� �ٲٴ� �ִ�
    [SerializeField]
    MeshRenderer[] playerMeshs;
    [SerializeField]
    Material baseMat;
    [SerializeField]
    Material invincibillityMat;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        playerBoby = GetComponent<PlayerBoby>();
    }
    void Update()
    {

        // �� �������� �ִ�
        Vector3 dir = new Vector3(movement.Dir.z, 0, movement.Dir.x);
        dir.z = -dir.z;
        playerSphere.Rotate(dir * rotateWeight * movement.Speed * Time.deltaTime, Space.World);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, ~LayerMask.NameToLayer("Platform")))
            head.rotation = RotateToTarget(new Vector2(transform.position.x, transform.position.z), new Vector2(hit.point.x, hit.point.z));


    }
    public void NonHitRender()
    {
            electricEndPoint.localPosition = new Vector3(playerBoby.Distance, 0,0);

    }
    public void HitRender(RaycastHit hit)
    {
        electricEndPoint.position = hit.point;
    }
    public static Quaternion RotateToTarget(Vector2 owner, Vector2 target, float weight = 0)
    { 
        float angle = Mathf.Atan2(target.y - owner.y, target.x - owner.x) * 180f / Mathf.PI;

        if(owner.y < target.y)
            return Quaternion.Euler(0, 360 - angle + 90, 0);
        else
            return Quaternion.Euler(0, -angle + 90, 0);

    }

    public static Vector2 GetPosFromAngle(float angle, float dist)
    {
        float y = Mathf.Sin(angle) * dist;
        float x = Mathf.Cos(angle) * dist;
        return new Vector2(x, y);
    }
    public void RenderInvincibillity()
    {
        foreach (MeshRenderer mesh in playerMeshs)
            mesh.material = invincibillityMat;
    }
    public void EndRenderInvincibillity()
    {
        foreach (MeshRenderer mesh in playerMeshs)
            mesh.material = baseMat;
    }
}
