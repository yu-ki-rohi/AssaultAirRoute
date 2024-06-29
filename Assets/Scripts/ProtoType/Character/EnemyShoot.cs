using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private CharacterBase _characterBase;
    [SerializeField] private GameObject _bullet;
    [SerializeField, Range(0.0f, 5.0f)] private float _diffRange = 0.5f;
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_keepInView != null)
        {
            if (player == null)
            {
                GameObject playerObject = _keepInView.Player;
                if (playerObject != null)
                {
                    player = playerObject.transform;
                }

            }
            else
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
                GameObject bullet = Instantiate(_bullet, transform.position, Quaternion.identity, route);
                bullet.GetComponent<BulletMove>().Init(_characterBase.Atk, gameObject, route);
                bullet.transform.forward = transform.forward;
            }
        }
    }
}
