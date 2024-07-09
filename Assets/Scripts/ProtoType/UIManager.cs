using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Image _hpBar;
    private int _maxHp = 1;
    private int _currentHp = 1;
    private int _displayHp;
    public int MaxHp { set { _maxHp = value;} }
    public int CurrentHp { set { _currentHp = value; _displayHp = _currentHp; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    public void ReflectCurrentHP(int currentHp)
    {
        _currentHp = currentHp;
        if(_maxHp > 0)
        {
            _hpBar.fillAmount = (float)_currentHp / (float)_maxHp;
        }
    }

    public void ReflectBounty(int bounty)
    {
        _scoreText.text = bounty.ToString();
    }
}
