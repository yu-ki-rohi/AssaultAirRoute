using UnityEngine;

public class CubeMove : MonoBehaviour
{
    public float moveSpeed = 5f; // �ړ����x

    void Update()
    {
        // ���������Ɛ��������̓��͂��擾
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // A�{�^���ŏ�Ɉړ��AD�{�^���ŉ��Ɉړ�
        float upDownInput = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            upDownInput = 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            upDownInput = -1f;
        }

        // �e�����̈ړ��x�N�g�����ʂɌv�Z
        Vector3 horizontalMovement = new Vector3(horizontalInput, 0, 0) * moveSpeed * Time.deltaTime;
        Vector3 verticalMovement = new Vector3(0, 0, verticalInput) * moveSpeed * Time.deltaTime;
        Vector3 upDownMovement = new Vector3(0, upDownInput, 0) * moveSpeed * Time.deltaTime;

        // �����̂��ړ�
        transform.Translate(horizontalMovement);
        transform.Translate(verticalMovement);
        transform.Translate(upDownMovement);
    }
}
