using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideAndFloat : MonoBehaviour
{
    public Vector3 targetPosition; // 目標位置
    public float slideSpeed = 1f; // スライド速度
    public float floatSpeed = 0.5f; // 浮遊速度
    public float floatAmount = 0.5f; // 浮遊幅
    public float minSpeed = 0.1f; // 最小スライド速度

    private Vector3 initialPosition; // 初期位置
    private float startY; // 浮遊の基準となるY座標

    void Start()
    {
        // 初期位置を設定
        initialPosition = transform.position;
        transform.position = new Vector3(initialPosition.x + targetPosition.x, initialPosition.y + targetPosition.y, initialPosition.z);

        // 浮遊の基準Y座標を設定
        startY = transform.position.y;
    }

    void Update()
    {
        // スライド処理
        float distance = Vector3.Distance(transform.position, initialPosition);
        float currentSpeed = Mathf.Max(slideSpeed * (distance / Vector3.Distance(targetPosition, initialPosition)), minSpeed);
        transform.position = Vector3.MoveTowards(transform.position, initialPosition, currentSpeed * Time.deltaTime);

        // 浮遊処理
        float floatOffset = Mathf.Sin(Time.time * floatSpeed) * floatAmount;
        transform.position = new Vector3(transform.position.x, startY + floatOffset, transform.position.z);
    }
}
