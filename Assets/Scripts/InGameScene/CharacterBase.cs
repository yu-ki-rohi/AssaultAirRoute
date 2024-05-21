// yu-ki-rohi
// キャラクターの雛形

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    protected int _currentHp;
    [SerializeField] protected CharacterData _data;
    public int CurrentHp { get { return _currentHp; } }

    // Start is called before the first frame update
    virtual protected void Start()
    {
        if (_data != null)
        {
            _currentHp = _data.MAXHP;
        }
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        
    }

    protected void Damage(Collider collider)
    {

    }
}
