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
        FollowTarget(playerTransform);
    }
}
