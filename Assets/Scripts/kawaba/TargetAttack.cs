using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAttack : MonoBehaviour
{
    public float attackInterval = 3f; // 攻撃間隔
    public float attackRange = 10f; // 攻撃範囲
    public float attackDamage = 10f; // 攻撃ダメージ
    public float homingSpeed = 5f; // ホーミング速度

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
        // プレイヤーに攻撃する
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
            //// プレイヤーにダメージを与える
            //PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            //if (playerHealth != null)
            //{
            //    playerHealth.TakeDamage(attackDamage);
            //}

            // 攻撃終了
            isAttacking = false;
        }
    }
}
