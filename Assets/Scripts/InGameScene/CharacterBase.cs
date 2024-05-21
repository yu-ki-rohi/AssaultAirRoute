// yu-ki-rohi
// キャラクターの雛形

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [SerializeField] protected int currentHp;
    [SerializeField] protected int maxHp;
    [SerializeField] protected float speed;
    // Start is called before the first frame update
    virtual protected void Start()
    {
        
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        
    }
}
