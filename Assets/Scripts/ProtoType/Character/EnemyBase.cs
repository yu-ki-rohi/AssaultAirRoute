using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacterBase
{
    [SerializeField] GameObject _caputuredEnemy;
    override protected void Die(GameObject attacker)
    {
        if (attacker != null)
        {
            if(attacker.TryGetComponent(out Shoot shoot))
            {
                if (_caputuredEnemy != null)
                {
                    shoot.CaptureEnemy(transform, _caputuredEnemy);
                }
            }
            
        }
        base.Die(attacker);
    }
        
}
