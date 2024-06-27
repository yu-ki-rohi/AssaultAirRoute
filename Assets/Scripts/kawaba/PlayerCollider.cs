using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // �v���C���[�ɓ��������I�u�W�F�N�g���^�[�Q�b�g�ł���Δj������
        if (other.CompareTag("Target"))
        {
            Destroy(other.gameObject);
        }
    }
}
