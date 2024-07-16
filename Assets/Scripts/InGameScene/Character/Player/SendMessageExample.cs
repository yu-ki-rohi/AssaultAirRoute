using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SendMessageExample : MonoBehaviour
{
    [SerializeField] private float Speed = 2.5f;
    private Vector3 _velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // 通知を受け取るメソッド名は「On + Action名」である必要がある
    private void OnPlayerMove(InputValue value)
    {
        // MoveActionの入力値を取得
        var axis = value.Get<Vector2>();

        // 移動速度を保持
        _velocity = new Vector3(axis.x, axis.y,0 );
    }
    // Update is called once per frame
    void Update()
    {
        // オブジェクト移動
        transform.position += _velocity * Speed;
    }
}
