using UnityEngine;

public class WorldHPCanvas : MonoBehaviour
{
    Transform target;
    private void OnEnable()
    {
        target = Camera.main.transform;
    }
    void Update()
    {
        transform.LookAt(transform.position + (transform.position - target.position));
    }
}
