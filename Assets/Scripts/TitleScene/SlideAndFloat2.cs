using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideAndFloat2 : MonoBehaviour
{
    public Vector3 targetPosition; // 目標位置
    public float slideSpeed = 1f; // スライド速度
    public float floatSpeed = 0.5f; // 浮遊速度
    public float floatAmount = 0.5f; // 浮遊幅
    public float minSpeed = 0.1f; // 最小スライド速度

    private Vector3 initialPosition; // 初期位置
    private float startY; // 浮遊の基準となるY座標
    private bool reachedInitialPosition = false; // 初期位置に到達したかどうかのフラグ

    [SerializeField]
    private FadeInUI FI;

    void Start()
    {
        // 初期位置を設定
        initialPosition = transform.position;

        // 目標位置を初期位置からの相対位置に変更
        targetPosition = new Vector3(initialPosition.x - targetPosition.x, initialPosition.y - targetPosition.y, initialPosition.z);

        // オブジェクトを目標位置に初期化
        transform.position = targetPosition;

        // 浮遊の基準Y座標を設定
        startY = initialPosition.y;
    }

    void Update()
    {
        if (FI.TitleSel == true)
        {
            if (!reachedInitialPosition)
            {
                // スライド処理（上から下に移動）
                float distance = Vector3.Distance(transform.position, initialPosition);
                float currentSpeed = Mathf.Max(slideSpeed * (distance / Vector3.Distance(targetPosition, initialPosition)), minSpeed);
                transform.position = Vector3.MoveTowards(transform.position, initialPosition, currentSpeed * Time.deltaTime);

                // 初期位置に到達したかどうかを確認
                if (transform.position == initialPosition)
                {
                    reachedInitialPosition = true;
                    startY = transform.position.y; // 浮遊の基準Y座標を再設定
                }
            }
            else
            {
                // 浮遊処理
                float floatOffset = Mathf.Sin(Time.time * floatSpeed) * floatAmount;
                transform.position = new Vector3(transform.position.x, startY + floatOffset, transform.position.z);
            }
        }
    }
}
