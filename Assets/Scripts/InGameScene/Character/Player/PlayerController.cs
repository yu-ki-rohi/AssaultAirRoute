using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField]private float speed = 50f; // 最大移動速度
    [SerializeField]private float acceleration = 2f; // 加速度
    [SerializeField]private float tiltAmount = 0.5f; // 傾きの角度
    [SerializeField]private float tiltSpeed = 5f; // 傾く速度
    [SerializeField]private float maxSpeed = 10f;    //最大速度
    private Vector3 velocity; // 現在の速度
    private float targetTilt = 0f; // 目標の傾き
    private Rigidbody rgd;

    void Start()
    {
        rgd = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 上下左右の入力の取得（WASDキー）
        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            verticalInput += speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            verticalInput -= speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput -= speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            horizontalInput += speed;
        }

        // 入力方向のベクトル
        Vector3 inputDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized;

        // 加速度を適用
        velocity += inputDirection * acceleration * Time.deltaTime;

        // 最大速度を制限
        velocity = Vector3.ClampMagnitude(velocity, speed);
        velocity = rgd.velocity;
        speed = velocity.magnitude;

        // 速度が上限を超えている場合
        if (speed > maxSpeed)
        {
            // 速度ベクトルを正規化し、制限速度でスカラー倍
            Vector3 limitedVelocity = velocity.normalized * maxSpeed;

            // 速度を設定
            rgd.velocity = limitedVelocity;
        }
        // プレイヤーの位置を更新
        transform.Translate(velocity * Time.deltaTime, Space.World);

        // プレイヤーの位置を画面内に制限
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, -5.61f, 5.62f); // 画面の左右端の座標に制限
        newPosition.y = Mathf.Clamp(newPosition.y, -0.5954228f, 3.0f); // 画面の上下端の座標に制限
        transform.position = newPosition;


        // 水平入力に基づいてZ軸を傾ける
        targetTilt = -horizontalInput * tiltAmount;
        float tilt = Mathf.LerpAngle(transform.localEulerAngles.z, targetTilt, Time.deltaTime * tiltSpeed);
        transform.localRotation = Quaternion.Euler(0f, 0f, tilt);

        // 入力がない場合、速度を減衰させる
        if (inputDirection == Vector3.zero)
        {
            velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime * acceleration);
        }
    }
}
