using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdCameraChase : MonoBehaviour
{
    public Transform player; // �v���C���[��Transform
    public float followSpeed = 5f; // �Ǐ]���x
    public Vector3 fixedPosition; // �J�������Œ肳�����W
    public float moveRange = 10f; // �v���C���[���J�����̒��Ŏ��R�Ɉړ��ł���͈͂̔��a

    private bool isFixed = false; // �J�������Œ肳��Ă��邩�ǂ����̃t���O

    void LateUpdate()
    {
        if (player != null)
        {
            if (!isFixed)
            {
                // �v���C���[�̈ʒu�ɏ��X�ɒǏ]
                Vector3 targetPosition = player.position;
                targetPosition.y = transform.position.y; // �J�����̍������Œ�
                transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

                // �J�������Œ肳���������`�F�b�N
                if (Vector3.Distance(transform.position, fixedPosition) < moveRange)
                {
                    isFixed = true;
                }
            }
            else
            {
                // �J�������Œ�ʒu�ɐݒ�
                transform.position = fixedPosition;

                // �v���C���[���J�����͈͓̔��œ�����悤�ɂ���
                Vector3 direction = player.position - transform.position;
                float distance = direction.magnitude;
                if (distance > moveRange)
                {
                    player.position = transform.position + direction.normalized * moveRange;
                }
            }
        }
    }
}
