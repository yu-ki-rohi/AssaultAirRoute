using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;
public class PlayerController : MonoBehaviour
{
    public float Speed = 0.07f; // 移動速度 
    private Vector3 _velocity;
    public float tiltAmount = 15f; // 傾きの角度 
    public float tiltSpeed = 5f; // 傾きの速度 
    Vector2 minBounds; // 移動制限の最小値 
    Vector2 maxBounds; // 移動制限の最大値 
    private Vector3 moveDirection = Vector3.zero;
    private Quaternion targetRotation; // ターゲットの回転 
    private Rigidbody rb;
    void Start()
    {
        targetRotation = transform.rotation; // 初期の回転を設定 
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // 重力を無効化 
    }
    void Update()
    {
        // オブジェクト移動
        transform.position += _velocity * Speed;
        
    }
    public void OnPlayerMove(InputAction.CallbackContext context)
    {
        // MoveActionの入力値を取得
        var axis = context.ReadValue<Vector2>();

        // 移動速度を保持
        _velocity = new Vector3(axis.x, axis.y, 0);
    }

    Vector3 ClampPositionToBounds(Vector3 position)
    {
        minBounds.x = -5.63f;
        minBounds.y = -1.71f;
        maxBounds.x = 5.79f;
        maxBounds.y = 1.88f;
        // 指定範囲内に位置を制限 
        float clampedX = Mathf.Clamp(position.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(position.y, minBounds.y, maxBounds.y);
        return new Vector3(clampedX, clampedY, position.z);
    }
}