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
    private bool Moov = false;
    public float acceleration = 5f; // 加速度
    [SerializeField]
    private BlinkUI Dis;
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // カメラの後ろにオブジェクトを配置
        startPosition = cameraTransform.position - cameraTransform.forward * 2f; // カメラの後ろに2ユニット
        transform.position = startPosition;

        // カメラの前の位置を設定 (Y軸方向に-2f移動)
        endPosition = cameraTransform.position + cameraTransform.forward * 6f; // カメラの前に10ユニット
        endPosition.y -= 2f; // Y軸方向に-2f移動
    }

    void Update()
    {
        // スライドの開始条件（例：UIが消えたら）
        if (Dis.UIBlinkdisappear && !isSliding && !Moov)
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

            // スライドが終了したかどうかをチェック
            if (elapsedTime >= slideDuration)
            {
                isSliding = false;
                Moov = true;
            }
        }

        // 移動処理
        if (Moov)
        {
            Vector3 movement = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                movement += Vector3.up;
            }
            if (Input.GetKey(KeyCode.S))
            {
                movement += Vector3.down;
            }
            if (Input.GetKey(KeyCode.A))
            {
                movement += Vector3.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                movement += Vector3.right;
            }

            rb.AddForce(movement * acceleration, ForceMode.Acceleration);

            Vector3 newPosition = transform.position;
            newPosition.x = Mathf.Clamp(newPosition.x, -3.84f, 3.49f); // 画面の左右端の座標に制限
            newPosition.y = Mathf.Clamp(newPosition.y, -0.8f, 1.66f); // 画面の上下端の座標に制限
            transform.position = newPosition;
        }
    }
}
