 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
   
    
public class BossGunFiring : MonoBehaviour
{
    public GameObject shellPrefab;
    public int count;
    public int speed;
    void Update()
    {
        count += 1;
        OnAnimationEnd();
        // �i�|�C���g�j
        // �U�O�t���[�����ƂɖC�e�𔭎˂���


    }
    public void OnAnimationEnd()
    {int rnd = Random.Range(0, 5);�@// �� 1�`9�͈̔͂Ń����_���Ȑ����l���Ԃ�
        if (count >= 300)
        {
            // �V�F�����w�肵���ʒu�Ɖ�]�ŃC���X�^���X��
            GameObject shell = Instantiate(shellPrefab, transform.position, Quaternion.Euler(0, 0, rnd * 45));
            transform.Rotate(0, 0, rnd * 45);
            Rigidbody shellRb = shell.GetComponent<Rigidbody>();

            // �e���͎��R�ɐݒ�
            shellRb.AddForce(transform.forward * speed);

            // �T�b��ɖC�e��j�󂷂�
            Destroy(shell, 5.0f);
            count = 0;
        }
        //�A�j���[�V�����I�����̏���
    }
}

