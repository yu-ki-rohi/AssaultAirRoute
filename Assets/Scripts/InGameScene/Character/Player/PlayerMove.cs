using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 3f; // 移動速度
    public float tiltAmount = 15f; // 傾きの角度
    public float tiltSpeed = 10f; // 傾く速度

    private float targetTilt = 0f; // 目標の傾き

    void Start()
    {

    }

    void Update()
    {
        // 上下左右の入力の取得（WASDキー）
        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            verticalInput += 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            verticalInput -= 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput -= 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            horizontalInput += 1f;
        }

        // 移動ベクトルの計算
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f) * speed * Time.deltaTime;

        // プレイヤーの位置を更新
        transform.Translate(movement, Space.World); // Space.Worldを指定して、ワールド座標系で移動する

        // プレイヤーの位置を画面内に制限
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, -3.84f, 3.49f); // 画面の左右端の座標に制限
        newPosition.y = Mathf.Clamp(newPosition.y, -0.8f, 1.66f); // 画面の上下端の座標に制限
        transform.position = newPosition;

        // 水平入力に基づいてZ軸を傾ける
        targetTilt = -horizontalInput * tiltAmount;
        float tilt = Mathf.LerpAngle(transform.localEulerAngles.z, targetTilt, Time.deltaTime * tiltSpeed);
        transform.localRotation = Quaternion.Euler(0f, 0f, tilt);
    }
}
