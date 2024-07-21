using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterBase
{
    [SerializeField] private const float _invincibleTime = 2.0f;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private GameObject[] _destoryWithPlayer;
    private float _invincibleTimer = 0.0f;
    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        if (_uiManager != null)
        {
            _uiManager.MaxHp = MaxHp;
            _uiManager.CurrentHp = CurrentHp;
        }
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        
        if(_invincibleTimer > 0.0f )
        {
            _invincibleTimer -= Time.deltaTime;
        }
    }

    public override void Damage(int power, GameObject attacker, bool isCaptureBullet = false)
    {
        if (_invincibleTimer <= 0.0f)
        {
            base.Damage(power, attacker);
            _invincibleTimer = _invincibleTime;
            if (_uiManager != null)
            {
                _uiManager.ReflectCurrentHP(_currentHp);
            }
        }  
    }

    protected override void Die(GameObject attacker, bool isCaptureBullet = false)
    {
        for(int i = 0;  i < _destoryWithPlayer.Length; i++)
        {
            if (_destoryWithPlayer[i] != null)
            {
                Destroy(_destoryWithPlayer[i]);
            }
        }
        base.Die(attacker);
    }

    public void AddBounty(int bounty)
    {
        _data.BOUNTY += bounty;
        if(_uiManager != null)
        {
            _uiManager.ReflectBounty(_data.BOUNTY);
        }
    }

    public void Drain()
    {
        int newHp = _currentHp + _data.DRAIN;
        if( newHp > MaxHp)
        {
            newHp = MaxHp;
        }
        _currentHp = newHp;

        if (_uiManager != null)
        {
            _uiManager.ReflectCurrentHP(_currentHp);
        }
    }
}
