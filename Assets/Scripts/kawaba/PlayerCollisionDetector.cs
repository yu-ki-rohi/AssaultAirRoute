using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            TargetScript targetScript = other.GetComponent<TargetScript>();
            if (targetScript != null)
            {
                targetScript.OnEnterCollision(gameObject);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            TargetScript targetScript = other.GetComponent<TargetScript>();
            if (targetScript != null)
            {
                targetScript.OnStayCollision();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            TargetScript targetScript = other.GetComponent<TargetScript>();
            if (targetScript != null)
            {
                targetScript.OnExitCollision();
            }
        }
    }
}
