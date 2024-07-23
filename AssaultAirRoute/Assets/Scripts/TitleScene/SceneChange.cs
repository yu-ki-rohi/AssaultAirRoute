using UnityEngine;
public class SceneChange : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.EntryAnimator = GetComponent<Animator>();
    }

    public void ChangePhase()
    {
       GameManager.Instance.ChangePhase();
    }

    public void ChangeBossPhase()
    {
        GameManager.Instance.ChangeBossPhase();
    }

    public void ChangeScene()
    {
        SceneController.Instance.ChangeScene();
    }
}
