using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossReticleMove : MonoBehaviour
{
    [SerializeField] private float _horizontallySup = 4.5f;
    [SerializeField] private float _varticalSup = 2.0f;
    [SerializeField] private float _horizontallyAdjust = 0.0f;
    [SerializeField] private float _varticalAdjust = 0.0f;
    [SerializeField] private float _aimingSmoothTime = 5.0f;
    [SerializeField] private float _shootingSmoothTime = 1.0f;
    [SerializeField] private float _buffer = 0.05f; 

    private bool _isShooting;
    private int _targetId = 0;
    private Vector3[] _targetPositions;

    private Vector3 _velocity = Vector3.zero;

    public bool IsShooting { get { return _isShooting; } }
    // Start is called before the first frame update
    void Start()
    {
        _targetPositions = new Vector3[8]{
            new Vector3(-_horizontallySup +_horizontallyAdjust, _varticalSup + _varticalAdjust, 0),
            new Vector3(-_horizontallySup +_horizontallyAdjust, _varticalAdjust, 0),
            new Vector3(-_horizontallySup +_horizontallyAdjust, -_varticalSup + _varticalAdjust, 0),
            new Vector3(_horizontallyAdjust, -_varticalSup + _varticalAdjust, 0),
            new Vector3(_horizontallySup +_horizontallyAdjust, -_varticalSup + _varticalAdjust, 0),
            new Vector3(_horizontallySup +_horizontallyAdjust, _varticalAdjust, 0),
            new Vector3(_horizontallySup +_horizontallyAdjust, _varticalSup + _varticalAdjust, 0),
            new Vector3(_horizontallyAdjust, _varticalSup + _varticalAdjust, 0)
        };

        _isShooting = true;
        ChangeTargetPosition();

    }

    // Update is called once per frame
    void Update()
    {
        float smoothTime;
        if (_isShooting)
        {
            smoothTime = _shootingSmoothTime;
        }
        else
        {
            smoothTime = _aimingSmoothTime;
        }

        transform.position = Vector3.SmoothDamp(
            transform.position,
            transform.parent.TransformPoint(_targetPositions[_targetId]),
            ref _velocity,
            smoothTime);

        float distance = (transform.parent.TransformPoint(_targetPositions[_targetId]) - transform.position).magnitude;
        
        if (distance < _buffer)
        {
            ChangeTargetPosition();
        }
    }

    private void ChangeTargetPosition()
    {
        if(_isShooting)
        {
            int nextId;
            do
            {
                nextId = Random.Range(0, 8);
            } while (_targetId == nextId);
            _targetId = nextId;
        }
        else
        {
            if(_targetId % 2 == 0)
            {
                int nextId = Random.Range(1, 4);
                _targetId = (_targetId + nextId * 2) % 8;
            }
            else
            {
                _targetId = (_targetId + 4) % 8;
            }
        }

        Debug.Log(_targetId);
        _isShooting = !_isShooting;
    }
}
