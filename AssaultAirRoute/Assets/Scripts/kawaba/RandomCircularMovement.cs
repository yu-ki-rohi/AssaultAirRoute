using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCircularMovement : MonoBehaviour
{
    public Transform player; // �v���C���[��Transform
    public float distanceFromPlayer = 10f; // �v���C���[����̋���
    public float movementSpeed = 2f; // �ړ����x
    public float radius = 5f; // �~�̔��a
    public float minDistanceBetweenTargets = 3f; // �^�[�Q�b�g���m�̍ŏ�����

    private Vector3 centerPosition; // �~�̒��S�ʒu
    private Vector3 movementAxis; // �ړ���
    private float rotationScale; // ��]���x�̃X�P�[��

    void Start()
    {
        // �~�̒��S�ʒu��ݒ�
        centerPosition = player.position + player.forward * distanceFromPlayer;

        // �����_���Ȉړ�����ݒ�
        movementAxis = Random.onUnitSphere;

        // �����_���ȉ�]���x�̃X�P�[����ݒ�
        rotationScale = Random.Range(0.5f, 2f); // ���x�̒������K�v�ɉ����Ĕ͈͂𒲐����Ă�������
    }

    void Update()
    {
        // �v���C���[�̑O�Ɉʒu������
        transform.position = player.position + player.forward * distanceFromPlayer;

        // �~��`���悤�Ɉړ�
        MoveInCircularPath();

        // �I�u�W�F�N�g���m�̋�����ۂ�
        KeepDistanceBetweenTargets();
    }

    void MoveInCircularPath()
    {
        // �~�^�����s��
        float angle = Time.time * rotationScale * movementSpeed; // �p���x�𒲐�
        Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
        transform.position = centerPosition + Quaternion.FromToRotation(Vector3.forward, movementAxis) * offset;
    }

    void KeepDistanceBetweenTargets()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");

        foreach (GameObject otherTarget in targets)
        {
            if (otherTarget != gameObject) // �������g�ȊO�̃^�[�Q�b�g�ɑ΂��Ă̂ݏ���
            {
                float distance = Vector3.Distance(transform.position, otherTarget.transform.position);
                if (distance < minDistanceBetweenTargets)
                {
                    Vector3 direction = transform.position - otherTarget.transform.position;
                    direction.Normalize();
                    transform.position += direction * (minDistanceBetweenTargets - distance) * Time.deltaTime;
                }
            }
        }
    }
}
