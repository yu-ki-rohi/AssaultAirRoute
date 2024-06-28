using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class BulletMove : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float timer = 3.0f;
    private Transform _route;
    private GameObject _player;
    private int _power;
    public Transform Route { get { return _route; } }
    public GameObject GetPlayer { get { return _player; } }
    public int Power { get { return _power; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //transform.localPosition += _route.rotation * transform.forward * speed * Time.deltaTime;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }

    public void Init(Transform route, GameObject player, int power)
    {
        _route = route;
        _player = player;
        _power = power;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            if(other.TryGetComponent(out CharacterBase enemyBase))
            {
                enemyBase.Damage(_power, _player);
                Destroy(gameObject);
            }
        }
    }

}
