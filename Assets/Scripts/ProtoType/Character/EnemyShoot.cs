// yu-ki-rohi
// 敵の射撃に関するスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    // 攻撃の際に参照するステータス
    [SerializeField] private ReferStatus _referStatus;

    // キャラクター情報を管理しているコンポーネント
    [SerializeField] private CharacterBase _characterBase;

    // 発射する場所
    [SerializeField] private Transform _firePosition;

    // 狙う対象
    // プレイヤーを設定しない場合
    [SerializeField] private Transform _fireTarget;

    // このコンポーネントで対象を注目させるか
    [SerializeField] private bool _lookAtTarget = true;

    // 発射する弾のオブジェクト
    [SerializeField] private GameObject _bullet;

    // 射撃をするまでのクールタイムに追加する時間の幅
    // これがないと敵の射撃が一斉に行われてしまうため
    [SerializeField, Range(0.0f, 5.0f)] private float _diffRange = 0.5f;

    // 弾の速度などを設定
    [SerializeField] private BulletSetting _bulletSetting;
    private Transform player;
    private KeepInView _keepInView;
    private float _coolTimer;
    // Start is called before the first frame update
    void Start()
    {
        _keepInView = GetComponent<KeepInView>();

        if (_characterBase != null)
        {
            // 初回発射までの時間をセット
            float diff = Random.Range(0.0f, _diffRange);
            _coolTimer = _characterBase.CoolTime + diff;

            // 攻撃の参照先をセット
            _referStatus.SetAttacks(_characterBase);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            // KeepInViewがプレイヤー情報を持ってる場合はコピー
            if (_keepInView != null)
            {
                GameObject playerObject = _keepInView.Player;
                if (playerObject != null)
                {
                    player = playerObject.transform;
                }
            }
            
            // ターゲットが登録されており、
            // 注目させる場合は
            // 注目対象に向ける
            if(_fireTarget != null && _lookAtTarget)
            {
                transform.LookAt(_fireTarget.position);
            }
        }
        else
        {
            if(_lookAtTarget)
            {
                transform.LookAt(player, Vector3.up);
            }
            
        }
        

        // 弾発射処理
        if (_keepInView != null && player != null)
        {
            if (_coolTimer > 0)
            {
                _coolTimer -= Time.deltaTime;
            }
            else
            {
                float diff = Random.Range(0.0f, _diffRange);
                _coolTimer = _characterBase.CoolTime + diff;
                Transform route = _keepInView.DesiredPosition;
                Transform firePosition = transform;
                if (_firePosition != null)
                {
                    firePosition = _firePosition;
                }
                GameObject bullet = Instantiate(_bullet, firePosition.position, Quaternion.identity, route);

                BulletMove bulletMove = bullet.GetComponent<BulletMove>();
                bulletMove.Init(_referStatus.GetAttack((int)_referStatus._referAttackType), gameObject, route);
                if(_bulletSetting.useSetting)
                {
                    bulletMove.Setting(_bulletSetting.speed, _bulletSetting.existTime);
                }
                
                if (_firePosition != null)
                {
                    bullet.transform.forward = (player.position - _firePosition.position).normalized;
                }
                else
                {
                    bullet.transform.forward = transform.forward;
                }

            }
        }
        else if (_fireTarget != null)
        {
            if (_coolTimer > 0)
            {
                _coolTimer -= Time.deltaTime;
            }
            else
            {
                float diff = Random.Range(0.0f, _diffRange);
                _coolTimer = _characterBase.CoolTime + diff;

                Transform firePosition = transform;
                if (_firePosition != null)
                {
                    firePosition = _firePosition;
                }
                GameObject bullet = Instantiate(_bullet, firePosition.position, Quaternion.identity);

                BulletMove bulletMove = bullet.GetComponent<BulletMove>();
                bulletMove.Init(_referStatus.GetAttack((int)_referStatus._referAttackType), gameObject);
                if (_bulletSetting.useSetting)
                {
                    bulletMove.Setting(_bulletSetting.speed, _bulletSetting.existTime);
                }

                if (_firePosition != null)
                {
                    bullet.transform.forward = (_fireTarget.position - _firePosition.position).normalized;
                }
                else
                {
                    bullet.transform.forward = transform.forward;
                }
                

            }

        }
    }

    private void Fire()
    {
        float diff = Random.Range(0.0f, _diffRange);
        _coolTimer = _characterBase.CoolTime + diff;
        Transform route = _keepInView.DesiredPosition;
        Transform firePosition = transform;
        if (_firePosition != null)
        {
            firePosition = _firePosition;
        }
        GameObject bullet = Instantiate(_bullet, firePosition.position, Quaternion.identity, route);
        bullet.GetComponent<BulletMove>().Init(_characterBase.Atk, gameObject, route);
        if (_firePosition != null)
        {
            bullet.transform.forward = (player.position - _firePosition.position).normalized;
        }
        else
        {
            bullet.transform.forward = transform.forward;
        }
    }
}
