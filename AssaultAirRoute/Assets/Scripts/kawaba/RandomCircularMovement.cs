using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCircularMovement : MonoBehaviour
{
    public Transform player; // プレイヤーのTransform
    public float distanceFromPlayer = 10f; // プレイヤーからの距離
    public float movementSpeed = 2f; // 移動速度
    public float radius = 5f; // 円の半径
    public float minDistanceBetweenTargets = 3f; // ターゲット同士の最小距離

    private Vector3 centerPosition; // 円の中心位置
    private Vector3 movementAxis; // 移動軸
    private float rotationScale; // 回転速度のスケール

    void Start()
    {
        // 円の中心位置を設定
        centerPosition = player.position + player.forward * distanceFromPlayer;

        // ランダムな移動軸を設定
        movementAxis = Random.onUnitSphere;

        // ランダムな回転速度のスケールを設定
        rotationScale = Random.Range(0.5f, 2f); // 速度の調整が必要に応じて範囲を調整してください
    }

    void Update()
    {
        // プレイヤーの前に位置させる
        transform.position = player.position + player.forward * distanceFromPlayer;

        // 円を描くように移動
        MoveInCircularPath();

        // オブジェクト同士の距離を保つ
        KeepDistanceBetweenTargets();
    }

    void MoveInCircularPath()
    {
        // 円運動を行う
        float angle = Time.time * rotationScale * movementSpeed; // 角速度を調整
        Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
        transform.position = centerPosition + Quaternion.FromToRotation(Vector3.forward, movementAxis) * offset;
    }

    void KeepDistanceBetweenTargets()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");

        foreach (GameObject otherTarget in targets)
        {
            if (otherTarget != gameObject) // 自分自身以外のターゲットに対してのみ処理
            {
                float distance = Vector3.Distance(transform.position, otherTarget.transform.position);
                if (distance < minDistanceBetweenTargets)
                {
                    Vector3 direction = transform.position - otherTarget.transform.position;
                    direction.Normalize();
                    transform.position += direction * (minDistanceBetweenTargets - distance) * Time.deltaTime;
                }
            }
        }
    }
}
