using UnityEngine;

// 同じGameObject下に入ることで

public class KeepInView : MonoBehaviour
{
    public float buffer = 0.1f;  // カメラの端からのバッファ距離
    public float smoothTime = 0.2f;  // スライドする時間
    public float distanceFromPlayer = 5.0f;  // プレイヤーからの距離
    public float verticalMovementAmplitude = 0.5f;  // 上下運動の振幅
    public float verticalMovementSpeed = 1.0f;  // 上下運動の速度

    [SerializeField] private float _up = 0.0f;
    [SerializeField] private float _right = 0.0f;
    [SerializeField] private float _verticalRange = 10.0f;
    [SerializeField] private float _horizontallyRange = 20.0f;

    private Camera mainCamera;
    // @yu-ki-rohi
    // playerのTransform情報しか使ってないので、
    // GameObjectまでは必要ないような気がしますね
    // そこでどれくらい差が出るのかは分からないですが、
    // 他のスクリプトでは同じようなところで、
    // Transformにしていたので気になりました
    private GameObject player;
    private Transform desiredPosition;
    private bool isActive = false;  // 処理を有効にするフラグ
    private Vector3 targetPosition = Vector3.zero;
    private Vector3 velocity = Vector3.zero;

    public GameObject Player { get { return player; } }
    public Transform DesiredPosition { get { return desiredPosition; } }

    void Start()
    {
        mainCamera = Camera.main;

        if(_up == 0.0f)
        {
            _up = Random.Range(-_verticalRange, _verticalRange);
        }

        if(_right == 0.0f)
        {
            _right = Random.Range(-_horizontallyRange, _horizontallyRange);
        }

        // @yu-ki-rohi
        // 今回はStart内の処理なので、
        // そこまで目くじら立てるほどのことでもないですが
        // FindWithTagは重めの処理なのでなるべく避けたほうが良いです
        // 特に今回はActive化処理があるのでそこで受け渡しができると思います
#if false
        player = GameObject.FindWithTag("Player");
#endif
    }

    void LateUpdate()
    {
        if (isActive)
        {
#if true
            //if(player != null)
            //{
            //    transform.forward = (player.transform.position - transform.position).normalized;
            //}

            // プレイヤーの位置に基づいてターゲットオブジェクトの目標位置を計算
            if (desiredPosition != null)
            {
                targetPosition = desiredPosition.position + desiredPosition.forward * distanceFromPlayer;
                targetPosition += desiredPosition.up * _up;
                targetPosition += desiredPosition.right * _right;
            }

#else
            // プレイヤーの位置に基づいてターゲットオブジェクトの目標位置を計算
            Vector3 offset = (transform.position - desiredPosition.position).normalized * distanceFromPlayer;
            Vector3 desiredPosition = desiredPosition.position + offset;

            // カメラの描画範囲内にターゲットオブジェクトを配置するためのスクリーン座標を計算
            Vector3 screenPos = mainCamera.WorldToViewportPoint(desiredPosition);

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

            // 新しいターゲットポジションを計算
            targetPosition = mainCamera.ViewportToWorldPoint(screenPos);

            // 上下運動を追加
            targetPosition.y += Mathf.Sin(Time.time * verticalMovementSpeed) * verticalMovementAmplitude;

#endif

            // スムーズにターゲットポジションへ移動
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }

    public void ActivateKeepInView(GameObject target, Transform position)
    {
        player = target;
        desiredPosition = position;
        isActive = true;
    }

    public void DeactivateKeepInView()
    {
        isActive = false;
    }
}
