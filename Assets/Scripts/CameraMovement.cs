using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    Vector3 offset = Vector3.zero;
    public void FollowTarget(Transform t)
    {
        transform.position = t.position + offset;
    }

    private void Update()
    {

        if (TimeCalculator.instance.leftT < 0)
            EndGameMovement();
        FollowTarget(playerTransform);
    }

    public void EndGameMovement()
    {

        if( Camera.main.orthographic != false)
            Camera.main.orthographic = false;
        offset += new Vector3(0, 5, -4) * 2 ;
    }
}
