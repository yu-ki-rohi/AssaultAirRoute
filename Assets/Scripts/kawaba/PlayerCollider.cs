using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // プレイヤーに当たったオブジェクトがターゲットであれば破棄する
        if (other.CompareTag("Target"))
        {
            Destroy(other.gameObject);
        }
    }
}
