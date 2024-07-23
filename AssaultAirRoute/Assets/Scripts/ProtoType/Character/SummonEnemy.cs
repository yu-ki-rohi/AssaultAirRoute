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
                Summon();
            }
        }
    }

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
        

        if (enemy.TryGetComponent(out KeepInView keepInView))
        {
            float diff = Random.Range(-_buffer, _buffer);
            keepInView.ActivateKeepInView(_player, _desiredPosition, _smoothTime, _baseDistanceFromPlayer + diff);
        }

    }
}
