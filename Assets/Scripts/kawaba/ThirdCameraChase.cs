using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdCameraChase : MonoBehaviour
{
    public Transform player; // プレイヤーのTransform
    public float followSpeed = 5f; // 追従速度
    public Vector3 fixedPosition; // カメラが固定される座標
    public float moveRange = 10f; // プレイヤーがカメラの中で自由に移動できる範囲の半径

    private bool isFixed = false; // カメラが固定されているかどうかのフラグ

    void LateUpdate()
    {
        if (player != null)
        {
            if (!isFixed)
            {
                // プレイヤーの位置に徐々に追従
                Vector3 targetPosition = player.position;
                targetPosition.y = transform.position.y; // カメラの高さを固定
                transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

                // カメラが固定される条件をチェック
                if (Vector3.Distance(transform.position, fixedPosition) < moveRange)
                {
                    isFixed = true;
                }
            }
            else
            {
                // カメラを固定位置に設定
                transform.position = fixedPosition;

                // プレイヤーがカメラの範囲内で動けるようにする
                Vector3 direction = player.position - transform.position;
                float distance = direction.magnitude;
                if (distance > moveRange)
                {
                    player.position = transform.position + direction.normalized * moveRange;
                }
            }
        }
    }
}
