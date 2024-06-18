using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    private Transform target;

    void Start()
    {
        // �ŏ��̃^�[�Q�b�g��ݒ�
        GameObject enemy = GameObject.FindWithTag("Enemy");
        if (enemy != null)
        {
            target = enemy.transform;
        }
    }

    void Update()
    {
        if (target != null)
        {
            // �G�̕����Ɍ������Ēe���ړ�
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            // �^�[�Q�b�g���Ȃ��ꍇ�͂��̂܂ܑO�i
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag=="Enemy")
        {
            Destroy(gameObject);
        }
    }
}
