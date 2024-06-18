using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    private Transform target;

    void Start()
    {
        // 最初のターゲットを設定
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
            // 敵の方向に向かって弾を移動
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            // ターゲットがない場合はそのまま前進
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
