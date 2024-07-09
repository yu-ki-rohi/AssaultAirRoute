using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAttack : MonoBehaviour
{
    public float attackInterval = 3f; // �U���Ԋu
    public float attackRange = 10f; // �U���͈�
    public float attackDamage = 10f; // �U���_���[�W
    public float homingSpeed = 5f; // �z�[�~���O���x

    private Transform player;
    private float lastAttackTime;
    private bool isAttacking = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastAttackTime = Time.time;
    }

    void Update()
    {
        if (Time.time - lastAttackTime > attackInterval && !isAttacking)
        {
            AttackPlayer();
            lastAttackTime = Time.time;
        }

        if (isAttacking)
        {
            HomingTowardsPlayer();
        }
    }

    void AttackPlayer()
    {
        // �v���C���[�ɍU������
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange)
        {
            isAttacking = true;
        }
    }

    void HomingTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * homingSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //// �v���C���[�Ƀ_���[�W��^����
            //PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            //if (playerHealth != null)
            //{
            //    playerHealth.TakeDamage(attackDamage);
            //}

            // �U���I��
            isAttacking = false;
        }
    }
}
