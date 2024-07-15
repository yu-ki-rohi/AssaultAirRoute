using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f; // 移動速度 
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
        rb.isKinematic = true; // 物理演算を無効化
    }
    void Update()
    {
        MovePlayer();
    }
    void MovePlayer()
    {
        // キーボード入力による移動方向の取得 
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        // ゲームコントローラーのボタン入力も取得 
        if (Input.GetButton("GamepadLeft"))
            moveHorizontal = -1;
        else if (Input.GetButton("GamepadRight"))
            moveHorizontal = 1;
        if (Input.GetButton("GamepadUp"))
            moveVertical = 1;
        else if (Input.GetButton("GamepadDown"))
            moveVertical = -1;
        // 最終的な移動方向を計算 
        moveDirection = new Vector3(moveHorizontal, moveVertical, 0).normalized;
        if (moveDirection.magnitude >= 0.1f)
        // 有効な入力がある場合にのみ処理 
        {
            // プレイヤーの新しい位置を計算 
            Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;
            // 新しい位置を指定範囲内に制限 
            newPosition = ClampPositionToBounds(newPosition);
            // プレイヤーの位置を設定 
            transform.position = newPosition;
            // プレイヤーの傾きの設定 
            float tilt = moveHorizontal * tiltAmount; targetRotation = Quaternion.Euler(0, 0, -tilt);
        }
        else
        {
            // 入力がない場合は水平に戻す 
            targetRotation = Quaternion.Euler(0, 0, 0);
        }
        // 傾きを適用 
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, tiltSpeed * Time.deltaTime);
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