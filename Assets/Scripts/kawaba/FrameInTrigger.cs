using UnityEngine;

public class FrameInTrigger : MonoBehaviour
{
    [SerializeField] Transform _targetParent;
    private GameObject[] targetObjects;  // フレームインさせたいオブジェクトの配列

    private void Awake()
    {
        targetObjects = new GameObject[_targetParent.childCount];
        for (int i = 0; i < _targetParent.childCount; i++)
        {
            targetObjects[i] = _targetParent.GetChild(i).gameObject;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Shoot shoot = other.GetComponentInParent<Shoot>();
            foreach (GameObject targetObject in targetObjects)
            {
                if(targetObject != null)
                {
                    KeepInView keepInView = targetObject.GetComponent<KeepInView>();
                    if (keepInView != null)
                    {

                        keepInView.ActivateKeepInView(other.gameObject, shoot.Route);
                    }
                }
               
            }
            gameObject.SetActive(false);  // トリガーを無効にする
        }
    }
}
