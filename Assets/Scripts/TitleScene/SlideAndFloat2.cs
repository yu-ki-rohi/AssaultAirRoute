using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideAndFloat2 : MonoBehaviour
{
    public Vector3 targetPosition2; // 目標位置
    public float slideSpeed2 = 1f; // スライド速度
    public float floatSpeed2 = 0.5f; // 浮遊速度
    public float floatAmount2 = 0.5f; // 浮遊幅
    public float minSpeed2 = 0.1f; // 最小スライド速度

    private Vector3 initialPosition2; // 初期位置
    private float startY2; // 浮遊の基準となるY座標

    void Start()
    {
        // 初期位置を設定
        initialPosition2 = transform.position;
        transform.position = new Vector3(initialPosition2.x + targetPosition2.x, initialPosition2.y - targetPosition2.y, initialPosition2.z);

        // 浮遊の基準Y座標を設定
        startY2 = transform.position.y;
    }

    void Update()
    {
        // スライド処理
        float distance = Vector3.Distance(transform.position, targetPosition2);
        float currentSpeed = Mathf.Max(slideSpeed2 * (distance / Vector3.Distance(initialPosition2, targetPosition2)), minSpeed2);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition2, currentSpeed * Time.deltaTime);

        // 浮遊処理
        float floatOffset = Mathf.Sin(Time.time * floatSpeed2) * floatAmount2;
        transform.position = new Vector3(transform.position.x, startY2 - floatOffset, transform.position.z);
    }
}
