using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class BulletMove : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float timer = 3.0f;
    private Transform _target;
    private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // transform.forward = (_target.localPosition - _player.localPosition).normalized;

        transform.localPosition += transform.forward * speed * Time.deltaTime;
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }

    public void Init(Transform target, GameObject player)
    {
        _target = target;
        _player = player;
    }

    public GameObject GetPlayer()
    {
        return _player;
    }
}
