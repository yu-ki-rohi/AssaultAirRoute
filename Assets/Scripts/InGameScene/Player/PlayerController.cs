using UnityEngine;

public class PlayerController: MonoBehaviour
{
    [SerializeField]
    int HP = 100;       //�v���C���[��HP
    [SerializeField]
    int Attack = 10;    //�v���C���[�̍U����
    public float acceleration = 50f;
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
        newPosition.x = Mathf.Clamp(newPosition.x, -3.84f, 3.49f); // ��ʂ̍��E�[�̍��W�ɐ���
        newPosition.y = Mathf.Clamp(newPosition.y, -0.8f, 1.66f); // ��ʂ̏㉺�[�̍��W�ɐ���
        transform.position = newPosition;

    }
}