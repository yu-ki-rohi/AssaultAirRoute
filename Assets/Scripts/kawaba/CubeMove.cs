using UnityEngine;

public class CubeMove : MonoBehaviour
{
    public float moveSpeed = 5f; // 移動速度

    void Update()
    {
        // 水平方向と垂直方向の入力を取得
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Aボタンで上に移動、Dボタンで下に移動
        float upDownInput = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            upDownInput = 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            upDownInput = -1f;
        }

        // 各方向の移動ベクトルを個別に計算
        Vector3 horizontalMovement = new Vector3(horizontalInput, 0, 0) * moveSpeed * Time.deltaTime;
        Vector3 verticalMovement = new Vector3(0, 0, verticalInput) * moveSpeed * Time.deltaTime;
        Vector3 upDownMovement = new Vector3(0, upDownInput, 0) * moveSpeed * Time.deltaTime;

        // 立方体を移動
        transform.Translate(horizontalMovement);
        transform.Translate(verticalMovement);
        transform.Translate(upDownMovement);
    }
}
