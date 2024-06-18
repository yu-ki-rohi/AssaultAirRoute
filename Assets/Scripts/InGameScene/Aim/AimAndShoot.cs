using UnityEngine;

public class AimAndShoot : MonoBehaviour
{
    public GameObject target; // 画面中心に配置した空のゲームオブジェクト
    public GameObject bulletPrefab; // 弾のプレハブ
    public Transform bulletSpawnPoint; // 弾の発射位置
    public float manualMoveSpeed = 15f; // 十字キーでの移動速度
    public float autoMoveSpeed = 10f; // 自動移動速度
    public float idleTimeThreshold = 0.1f; // 入力がなかったとみなす時間（秒）
    public float bulletSpeed = 10f; // 弾の速度

    private float lastInputTime; // 最後に入力があった時間

    void Update()
    {
        // 十字キーの入力を取得
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 入力があった場合、最後の入力時間を更新
        if (horizontalInput != 0 || verticalInput != 0)
        {
            lastInputTime = Time.time;
            Vector3 inputDirection = new Vector3(horizontalInput, verticalInput, 0).normalized;
            transform.position += inputDirection * manualMoveSpeed * Time.deltaTime;
        }
        else if (Time.time - lastInputTime >= idleTimeThreshold)
        {
            // 一定時間入力がなかった場合、滑らかにターゲットに向かって移動
            Vector3 targetPosition = target.transform.position;
            transform.position = Vector3.Lerp(transform.position, targetPosition, autoMoveSpeed * Time.deltaTime);
        }

        // スペースキーが押された場合、弾を発射
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // 弾のプレハブを生成
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // 弾に前方への力を加える
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = bulletSpawnPoint.forward * bulletSpeed;
        }
    }

    void Start()
    {
        // スタート時に最後の入力時間を現在の時間に設定
        lastInputTime = Time.time;
    }
}
