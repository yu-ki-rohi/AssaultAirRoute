using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLineShoot : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private CharacterBase _characterBase;
    [SerializeField] private Transform _firePosition;
    [SerializeField] private GameObject _bossReticle;
    [SerializeField] private BossReticleMove _reticleMove;
    [SerializeField, Range(0.0f, 1.0f)] private float _coolTime = 0.4f;
    private float _coolTimer = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        if(_bossReticle != null && _reticleMove == null)
        {
            Debug.Log("Serch");
            _reticleMove = _bossReticle.GetComponent<BossReticleMove>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_reticleMove == null)
        {
            Debug.Log("NULL");
            return;
        }

        if(_coolTimer > 0)
        {
            _coolTimer -= Time.deltaTime;
        }

        if(_reticleMove.IsShooting)
        {
            if(_coolTimer <= 0)
            {
               
                _coolTimer = _coolTime;

                Transform firePosition = transform;
                if (_firePosition != null)
                {
                    firePosition = _firePosition;
                }
                GameObject bullet = Instantiate(_bullet, firePosition.position, Quaternion.identity);
                bullet.GetComponent<BulletMove>().Init(_characterBase.Atk, gameObject);
                Vector3 dir = (_bossReticle.transform.position - firePosition.position).normalized;
                bullet.transform.forward = dir;
            }
        }
    }
}
