using UnityEngine;

public class PlayerController: MonoBehaviour
{
    [SerializeField]
    int HP = 100;       //プレイヤーのHP
    [SerializeField]
    int Attack = 10;    //プレイヤーの攻撃力
    public float acceleration = 5f;//加速度
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movement += Vector3.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += Vector3.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += Vector3.right;
        }

        rb.AddForce(movement * acceleration, ForceMode.Acceleration);

        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, -3.84f, 3.49f); // 画面の左右端の座標に制限
        newPosition.y = Mathf.Clamp(newPosition.y, -0.8f, 1.66f); // 画面の上下端の座標に制限
        transform.position = newPosition;

    }
}
