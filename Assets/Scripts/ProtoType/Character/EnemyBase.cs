using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacterBase
{
    [SerializeField] GameObject _caputuredEnemy;
    override protected void Die(GameObject attacker, bool isCaptureBullet = false)
    {
        if (attacker != null)
        {
            // キャプチャー処理
            if(attacker.TryGetComponent(out Shoot shoot))
            {
                if (_caputuredEnemy != null && isCaptureBullet)
                {
                    shoot.CaptureEnemy(transform, _caputuredEnemy);
                }
            }

            // バウンティー加算
            if (attacker.TryGetComponent(out Player player))
            {
                player.AddBounty(_data.BOUNTY);
                player.Drain();
            }

        }
        base.Die(attacker);
    }
    
    // 自爆メソッド
    public void Suicide(GameObject attacker)
    {
        base.Damage(_data.MAXHP, attacker);
    }
}
