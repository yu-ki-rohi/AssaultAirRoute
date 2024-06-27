using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public float buffer = 0.1f;  // カメラの端からのバッファ距離
    public float smoothTime = 0.2f;  // スライドする時間
    public float distanceFromCamera = 5.0f;  // カメラからの距離
    public float verticalMovementAmplitude = 0.5f;  // 上下運動の振幅
    public float verticalMovementSpeed = 1.0f;  // 上下運動の速度

    private Camera mainCamera;
    private bool isActive = false;  // 処理を有効にするフラグ
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (isActive)
        {
            Vector3 screenPos = mainCamera.WorldToViewportPoint(transform.position);

            if (screenPos.x < buffer)
            {
                screenPos.x = buffer;
            }
            else if (screenPos.x > 1 - buffer)
            {
                screenPos.x = 1 - buffer;
            }

            if (screenPos.y < buffer)
            {
                screenPos.y = buffer;
            }
            else if (screenPos.y > 1 - buffer)
            {
                screenPos.y = 1 - buffer;
            }

            // カメラの前に位置を設定
            screenPos.z = distanceFromCamera;

            targetPosition = mainCamera.ViewportToWorldPoint(screenPos);

            // 上下運動を追加
            targetPosition.y += Mathf.Sin(Time.time * verticalMovementSpeed) * verticalMovementAmplitude;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }

    public void ActivateKeepInView()
    {
        isActive = true;
    }

    public void DeactivateKeepInView()
    {
        isActive = false;
    }
}
