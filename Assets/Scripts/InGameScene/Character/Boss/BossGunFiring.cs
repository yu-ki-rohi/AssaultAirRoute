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
        // （ポイント）
        // ６０フレームごとに砲弾を発射する


    }
    public void OnAnimationEnd()
    {int rnd = Random.Range(0, 5);　// ※ 1～9の範囲でランダムな整数値が返る
        if (count >= 300)
        {
            // シェルを指定した位置と回転でインスタンス化
            GameObject shell = Instantiate(shellPrefab, transform.position, Quaternion.Euler(0, 0, rnd * 45));
            transform.Rotate(0, 0, rnd * 45);
            Rigidbody shellRb = shell.GetComponent<Rigidbody>();

            // 弾速は自由に設定
            shellRb.AddForce(transform.forward * speed);

            // ５秒後に砲弾を破壊する
            Destroy(shell, 5.0f);
            count = 0;
        }
        //アニメーション終了時の処理
    }
}

