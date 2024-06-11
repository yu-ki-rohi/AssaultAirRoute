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

    // ダメージを受ける処理
    // 威力を取得するために、引数でColliderを渡してください
    protected void Damage(Collider collider)
    {
        // colliderの威力を受け取る処理
        // ここではとりあえず仕様書に則り10
        int damage = 10;

        // HP減少処理
        _currentHp -= damage;
        if(_currentHp < 0)
        {
            _currentHp = 0;
        }
    }
}
