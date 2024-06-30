using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private ReferStatus _referStatus;
    [SerializeField] private CharacterBase _characterBase;
    [SerializeField] private Transform _firePosition;
    [SerializeField] private Transform _fireTarget;
    [SerializeField] private GameObject _bullet;
    [SerializeField, Range(0.0f, 5.0f)] private float _diffRange = 0.5f;
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
            float diff = Random.Range(0.0f, _diffRange);
            _coolTimer = _characterBase.CoolTime + diff;
            _referStatus.SetAttacks(_characterBase);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            if (_keepInView != null)
            {
                GameObject playerObject = _keepInView.Player;
                if (playerObject != null)
                {
                    player = playerObject.transform;
                }
            }
            
            if(_fireTarget != null)
            {
                transform.LookAt(_fireTarget.position);
            }

        }
        else
        {
            transform.LookAt(player, Vector3.up);
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
