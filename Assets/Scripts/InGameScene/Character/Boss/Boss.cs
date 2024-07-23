using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyBase
{
    override protected void Die(GameObject attacker, bool isCaptureBullet = false)
    {
        // バウンティー加算
        if (attacker.TryGetComponent(out Player player))
        {
            player.AddBounty(_data.BOUNTY);
            player.Drain();
        }
        SceneController.Instance.ReStart(); 
    }
}
