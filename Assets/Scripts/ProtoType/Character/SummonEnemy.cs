using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private float _coolTime = 20.0f;
    [SerializeField, Range(0.0f, 1.0f)] private float _summonStartRate = 0.5f;
    [SerializeField] private float _smoothTime = 1.0f;
    [SerializeField] private float _baseDistanceFromPlayer = 15.0f;
    [SerializeField] private float _buffer = 5.0f;
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _desiredPosition;
    [SerializeField] private CharacterBase _characterBase;
    [SerializeField] private Transform _summonPosition;
    [SerializeField] private Transform _parent;
    private float _coolTimer = 0.0f;

    //【雑魚自爆】
    // 自爆させる雑魚を管理するためのListを用意してください
    // privateで良いと思います

    //

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_enemies.Length <= 0 || 
            _player == null || 
            _desiredPosition == null ||
            _characterBase == null)
        {
            return;
        }

        if(_characterBase.CurrentHp < _characterBase.MaxHp * _summonStartRate)
        {
            if(_coolTimer > 0.0f)
            {
                _coolTimer -= Time.deltaTime;
            }
            else
            {
                // 召還
                Summon();
            }
        }
    }

    // 召還メソッド
    private void Summon()
    {
        _coolTimer = _coolTime;

        int id = Random.Range(0, _enemies.Length);
        GameObject enemy;

        Transform summonPosition = transform;
        if(_summonPosition != null)
        {
            summonPosition = _summonPosition;
        }

        if (_parent != null)
        {
            enemy = Instantiate(_enemies[id], summonPosition.position, Quaternion.identity, _parent);
        }
        else
        {
            enemy = Instantiate(_enemies[id], summonPosition.position, Quaternion.identity);
        }

        //【雑魚自爆】
        // ここでListに召還したenemyを追加

        //

        if (enemy.TryGetComponent(out KeepInView keepInView))
        {
            float diff = Random.Range(-_buffer, _buffer);
            keepInView.ActivateKeepInView(_player, _desiredPosition, _smoothTime, _baseDistanceFromPlayer + diff);
        }

    }

    //【雑魚自爆】
    // 召還した雑魚を一斉に倒すメソッド
    public void VanishEnemies(GameObject attacker)
    {
        // for文でList内の雑魚を殲滅してください
        // EnemyBaseにSuicideメソッドを用意したので、
        // それを使うとよいと思います
        // 単体の奴と群体の奴で、EnemyBaseを持っている
        // GameObjectが違う点にご注意ください
    }
}
