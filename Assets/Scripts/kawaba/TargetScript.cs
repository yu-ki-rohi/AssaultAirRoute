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
        // ����̏����������ɒǉ�
    }

    public void OnStayCollision()
    {
        Debug.Log("Target is within the BoxCollider and continues its specific action.");
        // �p���I�ȓ���̏����������ɒǉ�
    }

    public void OnExitCollision()
    {
        Debug.Log("Target exited the BoxCollider but continues KeepInView action.");
        // ����̏������I�����邽�߂̏����������ɒǉ�
    }
}
