using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody characterController;

    Vector3 dir = Vector2.zero;
    public Vector3 Dir { get => dir; private set { } }
    [SerializeField]
    float speed = 5f;
    public float Speed { get => speed; private set { } }

    private void Awake()
    {
        characterController =  GetComponent<Rigidbody>();
    }
    public void Moving()
    {
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");

        characterController.linearVelocity = dir * speed /** Time.deltaTime*/;
    }
}
