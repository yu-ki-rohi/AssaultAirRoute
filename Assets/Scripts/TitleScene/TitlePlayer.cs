using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePlayer : MonoBehaviour
{
    [SerializeField]
    private Transform cameraTransform; // カメラのTransformを設定
    [SerializeField]
    private float slideDuration = 1.0f; // スライドの所要時間
    private Vector3 startPosition; // 開始位置
    private Vector3 endPosition; // 終了位置
    private float elapsedTime = 0f; // 経過時間
    private bool isSliding = false; // スライド中かどうかのフラグ
    [SerializeField]
    private BlinkUI Dis;

    void Start()
    {
        // カメラの後ろにオブジェクトを配置
        startPosition = cameraTransform.position - cameraTransform.forward * 2f; // カメラの後ろに10ユニット
        transform.position = startPosition;

        // カメラの前の位置を設定 (Y軸方向に-0.5f移動)
        endPosition = cameraTransform.position + cameraTransform.forward * 10f; // カメラの前に2ユニット
        endPosition.y -= 2f; // Y軸方向に-0.5f移動
    }

    void Update()
    {
        // スライドの開始条件（例：スペースキーを押す）
        if (Dis.UIBlinkdisappear == true && !isSliding)
        {
            isSliding = true;
            elapsedTime = 0f;
        }

        // スライド中の処理
        if (isSliding)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / slideDuration;

            // 滑らかな動きのためにSmoothStepを使用
            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            // 線形補間を使ってオブジェクトの位置を更新
            transform.position = Vector3.Lerp(startPosition, endPosition, smoothT);
        }
    }
}
