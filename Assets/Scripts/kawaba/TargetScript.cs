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
            keepInView.ActivateKeepInView(target);
        }
        // “Á’è‚Ìˆ—‚ğ‚±‚±‚É’Ç‰Á
    }

    public void OnStayCollision()
    {
        Debug.Log("Target is within the BoxCollider and continues its specific action.");
        // Œp‘±“I‚È“Á’è‚Ìˆ—‚ğ‚±‚±‚É’Ç‰Á
    }

    public void OnExitCollision()
    {
        Debug.Log("Target exited the BoxCollider but continues KeepInView action.");
        // “Á’è‚Ìˆ—‚ğI—¹‚·‚é‚½‚ß‚Ìˆ—‚ğ‚±‚±‚É’Ç‰Á
    }
}
