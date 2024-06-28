using UnityEngine;

public class TargetScript : MonoBehaviour
{
    private KeepInView keepInView;

    void Start()
    {
        keepInView = GetComponent<KeepInView>();
    }

    public void OnEnterCollision(GameObject target)
    {
        Debug.Log("Target entered the BoxCollider and started its specific action.");
        if (keepInView != null)
        {
            //keepInView.ActivateKeepInView(target);
        }
        // 特定の処理をここに追加
    }

    public void OnStayCollision()
    {
        Debug.Log("Target is within the BoxCollider and continues its specific action.");
        // 継続的な特定の処理をここに追加
    }

    public void OnExitCollision()
    {
        Debug.Log("Target exited the BoxCollider but continues KeepInView action.");
        // 特定の処理を終了するための処理をここに追加
    }
}
