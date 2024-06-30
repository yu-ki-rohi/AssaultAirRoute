using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class BulletMove : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private float _timer = 3.0f;
    private Transform _route;
    private GameObject _attacker;
    private int _power;
    private bool _isCaptureBullet;
    public Transform Route { get { return _route; } }
    public GameObject GetAttacker { get { return _attacker; } }
    public int Power { get { return _power; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //transform.localPosition += _route.rotation * transform.forward * _speed * Time.deltaTime;
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            Destroy(gameObject);
        }
    }

    public void Init(int power,  GameObject attacker, Transform route = null, bool isCaptureBullet = false)
    {
        _power = power;
        _attacker = attacker;
        _route = route;
        _isCaptureBullet = isCaptureBullet;
    }

    public void Setting(float speed, float time)
    {
        _speed = speed;
        _timer = time;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == null || _attacker == null)
        { 
            return;
        }
        if(other.tag == "Enemy" && _attacker.tag == "Player")
        {
            if(other.TryGetComponent(out CharacterBase enemyBase))
            {
                enemyBase.Damage(_power, _attacker, _isCaptureBullet);
                Destroy(gameObject);
            }
            else
            {
                CharacterBase characterBase = other.GetComponentInParent<CharacterBase>();
                if(characterBase != null)
                {
                    characterBase.Damage(_power, _attacker, _isCaptureBullet);
                    Destroy(gameObject);
                }
            }
        }

        if (other.tag == "Player" && _attacker.tag == "Enemy")
        {
            if (other.TryGetComponent(out CharacterBase player))
            {
                player.Damage(_power, _attacker);
                Destroy(gameObject);
            }
        }
    }

}
