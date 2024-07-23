using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 各オブジェクトをプレイヤーに向けさせる
public class AimTarget : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] Vector3 _upVec = Vector3.up;
    // Start is called before the first frame update
    void Start()
    {
        _upVec = _upVec.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if(_target != null)
        {
            transform.LookAt(_target.position, _upVec);
        }
    }
}
