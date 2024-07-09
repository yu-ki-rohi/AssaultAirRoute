using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class HomingMissile : MonoBehaviour
{
   
    public Transform target; // 追尾するターゲット
    private float speed; // 弾の速度
    public float rotateSpeed = 10f; // 弾の回転速度（ホーミングを強化）
    public float lifeTime = 5f; // 弾の寿命

    private float timeSinceLaunch;
    


    private void Awake()
    {
       
    }
    void Start()
    {
        timeSinceLaunch = 0f;
    }

    void Update()
    {
        // 弾の寿命を管理
        timeSinceLaunch += Time.deltaTime;
        if (timeSinceLaunch >= lifeTime)
        {
            Destroy(gameObject);
            return;
        }

        if (target != null)
        {
            // ターゲットへの方向を計算
            Vector3 direction = target.position - transform.position;
            direction.Normalize();

            // ターゲットの方向に向く回転を計算
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);

        }

        // 前方に移動
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
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

