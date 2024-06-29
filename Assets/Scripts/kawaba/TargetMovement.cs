using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TargetMovement : MonoBehaviour
{
    // @yu-ki-rohi
    // playerのTransform取得、いちいち手付しなくても、
    // DetectCollide用意してPlayerに当たったら
    // その時に取得とかにするといいかも
    private Transform player; // プレイヤーのTransform
    public float distanceFromPlayer = 10f; // プレイヤーからの距離
    public float movementSpeed = 2f; // ターゲットの移動速度
    public float movementRange = 5f; // ターゲットの移動範囲
    public float minDistanceBetweenTargets = 3f; // ターゲット同士の最小距離
    public float maxMovementChangeInterval = 3f; // 移動変更の最大間隔
    public float homingDuration = 5f; // ホーミングの持続時間
    public float homingSpeed = 5f; // ホーミング時の速度

    private Vector3 targetPosition;
    private Vector3 movementDirection;
    private float lastMovementChangeTime;
    private bool isHoming = false;
    [SerializeField]
    private float homingStartTime;

    private EnemyArray _enemyArray;
    private GameObject[] _enemies;

    [SerializeField] private Transform _gatherPosition;
    private KeepInView _keepInView;
    [SerializeField] private CharacterBase _characterBase;
    [SerializeField] private GameObject _bullet;
    [SerializeField, Range(0.0f, 5.0f)] private float _diffRange = 0.5f;
    private float _coolTimer;

    void Start()
    {
        // ターゲットの初期位置を設定
        SetRandomTargetPosition();
        lastMovementChangeTime = Time.time;

        Invoke("StartHoming", homingStartTime);

        
        _enemyArray = GetComponentInParent<EnemyArray>();
        if (_enemyArray != null )
        {
            _enemies = _enemyArray.Enemys;
        }

        _keepInView = GetComponentInParent<KeepInView>();

        if(_characterBase != null )
        {
            float diff = Random.Range(0.0f, _diffRange);
            _coolTimer = _characterBase.CoolTime + diff;
        }
    }

    void Update()
    {
        if( _keepInView != null )
        {
            if(player == null)
            {
                GameObject playerObject = _keepInView.Player;
                if(playerObject != null)
                {
                    player = playerObject.transform;
                }
                
            }
            else
            {
                transform.LookAt(player, Vector3.up);
            }
        }
        if (!isHoming)
        {
            // これするとplayerの上下左右の動きに合わせて動いてしまうから、
            // ちょっとまずいかも
            // そもそも同じGameObject下に入ることで、
            // 相対座標で考えられるから、
            // 前方に位置し続けるための処理は必要ないと思う
            // これに関しては説明不足で申し訳ない

#if true
            // プレイヤーの前方に位置させる
            Vector3 forwardPosition = _gatherPosition.position + _gatherPosition.forward * distanceFromPlayer;
            transform.position = Vector3.Lerp(transform.position, forwardPosition, Time.deltaTime * movementSpeed);
#endif
            // ターゲット同士の距離を保つ
            KeepDistanceBetweenTargets();

            // 一定間隔でランダムな方向に移動する
            if (Time.time - lastMovementChangeTime > maxMovementChangeInterval)
            {
                lastMovementChangeTime = Time.time;
                SetRandomMovementDirection();
            }

            // ランダムな方向に移動
            transform.position += movementDirection * movementSpeed * Time.deltaTime;

            // 弾発射処理
            if (_keepInView != null && player != null)
            {
                if(_coolTimer > 0)
                {
                    _coolTimer -= Time.deltaTime;
                }
                else
                {
                    float diff = Random.Range(0.0f, _diffRange);
                    _coolTimer = _characterBase.CoolTime + diff;
                    Transform route = _keepInView.DesiredPosition;
                    GameObject bullet = Instantiate(_bullet, transform.position, Quaternion.identity, route);
                    bullet.GetComponent<BulletMove>().Init(_characterBase.Atk, gameObject, route);
                    bullet.transform.forward = transform.forward;
                }
            }
        }
        else
        {
            if (player != null)
            {
                // ホーミング中はプレイヤーに向かって移動
                Vector3 playerDirection = player.position - transform.position;
                //transform.position += playerDirection.normalized * homingSpeed * Time.deltaTime;
                transform.forward = playerDirection.normalized;
                transform.Translate(Vector3.forward * homingSpeed * Time.deltaTime);

                // ホーミングの持続時間が経過したらホーミングを終了
                if (Time.time - homingStartTime > homingDuration)
                {
                    isHoming = false;
                    SetRandomTargetPosition(); // ホーミングが終了したらランダムな位置に移動
                }
            }
                
        }

    }

    // ホーミングを開始するメソッド
    public void StartHoming()
    {
        isHoming = true;
        homingStartTime = Time.time;
    }

    void SetRandomTargetPosition()
    {
        float randomX = Random.Range(-movementRange, movementRange);
        float randomY = Random.Range(-movementRange, movementRange);
        float randomZ = Random.Range(-movementRange, movementRange);

        targetPosition = _gatherPosition.position + _gatherPosition.forward * distanceFromPlayer + new Vector3(randomX, randomY, randomZ);
        SetRandomMovementDirection();
    }

    void SetRandomMovementDirection()
    {
        movementDirection = Random.insideUnitSphere.normalized;
    }

    void KeepDistanceBetweenTargets()
    {
        // @yu-ki-rohi
        // この処理(FindGameObjectsWithTag)は結構重たいらしいから、
        // Updateで呼び出すのは避けたほうが良いかも
        // 対策として考えられるのは、
        // 1. 最初からひとまとまりとして用意
        // 2. DetectColliderを用意して当たったやつを追加
        // あたりかな
        

        foreach (GameObject otherTarget in _enemies)
        {
            if(otherTarget != null)
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

    void OnCollisionEnter(Collision collision)
    {  
        // ホーミング中にプレイヤーに衝突したら爆発
        if (collision.gameObject.tag =="Player" && isHoming)
        {
            if(_characterBase != null)
            {
                if (collision.gameObject.TryGetComponent(out CharacterBase characterBase))
                {
                    characterBase.Damage(_characterBase.SpecialAtk, null);
                    _characterBase.Damage(_characterBase.MaxHp, null);
                }
            }
                          
        }
    }
}