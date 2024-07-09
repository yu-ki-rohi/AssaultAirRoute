using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HomingMisaile2 : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public float rotateSpeed = 200f;
    public float orbitRadius = 3f; // 円の半径
    private Vector2 orbitCenter;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Target").transform;
        orbitCenter = target.position;
    }

    private void Update()
    {
        if (target == null) return;

        MoveInOrbit();
    }

    private void MoveInOrbit()
    {
        // ターゲットからの距離を保持
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget > orbitRadius)
        {
            // ターゲットの方向に移動
            MoveTowardsTarget();
        }
        else
        {
            // 円軌道に沿って移動
            Vector2 direction = ((Vector2)transform.position - orbitCenter).normalized;
            direction = new Vector2(-direction.y, direction.x); // 90度回転して円軌道に沿って移動

            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void MoveTowardsTarget()
    {
        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == target)
        {
            // ターゲットに当たった場合、弾とターゲットを消す
            Destroy(target.gameObject);
            Destroy(gameObject);
        }
    }
}
