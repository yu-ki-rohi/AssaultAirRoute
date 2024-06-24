using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 3f; // �ړ����x
    public float tiltAmount = 15f; // �X���̊p�x
    public float tiltSpeed = 10f; // �X�����x

    private float targetTilt = 0f; // �ڕW�̌X��

    void Start()
    {

    }

    void Update()
    {
        // �㉺���E�̓��͂̎擾�iWASD�L�[�j
        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            verticalInput += 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            verticalInput -= 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput -= 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            horizontalInput += 1f;
        }

        // �ړ��x�N�g���̌v�Z
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f) * speed * Time.deltaTime;

        // �v���C���[�̈ʒu���X�V
        transform.Translate(movement, Space.World); // Space.World���w�肵�āA���[���h���W�n�ňړ�����

        // �v���C���[�̈ʒu����ʓ��ɐ���
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, -3.84f, 3.49f); // ��ʂ̍��E�[�̍��W�ɐ���
        newPosition.y = Mathf.Clamp(newPosition.y, -0.8f, 1.66f); // ��ʂ̏㉺�[�̍��W�ɐ���
        transform.position = newPosition;

        // �������͂Ɋ�Â���Z�����X����
        targetTilt = -horizontalInput * tiltAmount;
        float tilt = Mathf.LerpAngle(transform.localEulerAngles.z, targetTilt, Time.deltaTime * tiltSpeed);
        transform.localRotation = Quaternion.Euler(0f, 0f, tilt);
    }
}
