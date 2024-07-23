using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    // @yu-ki-rohi
    // SerializeFieldとpublicが混在しているけど、
    // これは何か意図があるのでしょうか

    [SerializeField]
    
    public GameObject bulletPrefab; // 弾のプレハブ
    public Transform bulletSpawnPoint; // 弾の出現位置
    private float bulletSpeed; // 弾の速度
    public float lockOnRadius = 50f; // ターゲットロックオン範囲

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
       
        Transform target = FindClosestTarget();

        // @yu-ki-rohi
        // 流石に対象いないと弾発射できないのはあれな気がする
        // 生成自体はnullでもできるようにして、
        // targetの登録をnullか否かで切り替えるとよいかも
        // もしくはbulletにDetectColliderみたいなのをもたせて、
        // 弾自信でtargetを決められるようにしたほうがよいかも
        // まあこれプレイヤーの範疇だと思うし、
        // 今回わざわざ指摘するのもあれな気がしたけど、
        // 参考までに
        // 弾を生成し、発射位置を少し前方にオフセット
        Vector3 spawnPosition = bulletSpawnPoint.position + bulletSpawnPoint.forward * 1.5f;
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, bulletSpawnPoint.rotation);
        bullet.transform.forward = transform.forward;
        // 弾の初速度を設定
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = bulletSpawnPoint.forward * bulletSpeed;
        }
        if (target != null)
        {
            
            // 弾のホーミングミサイルスクリプトにターゲットを設定
            HomingMissile homingMissile = bullet.GetComponent<HomingMissile>();
            if (homingMissile != null)
            {
                homingMissile.target = target;
            }
        }
    }

    Transform FindClosestTarget()
    {
        // @yu-ki-rohi
        // この処理(FindGameObjectsWithTag)は結構重たいらしいから、
        // Updateで呼び出すのは避けたほうが良いかも
        // 対策はShoot内に書いてます
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        Transform closestTarget = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < minDistance && distance <= lockOnRadius)
            {
                closestTarget = target.transform;
                minDistance = distance;
            }
        }

        return closestTarget;
    }
}
