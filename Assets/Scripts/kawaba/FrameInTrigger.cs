using UnityEngine;

public class FrameInTrigger : MonoBehaviour
{
    public GameObject[] targetObjects;  // フレームインさせたいオブジェクトの配列

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject targetObject in targetObjects)
            {
                KeepInView keepInView = targetObject.GetComponent<KeepInView>();
                if (keepInView != null)
                {
                    keepInView.ActivateKeepInView(other.gameObject);
                }
            }
            gameObject.SetActive(false);  // トリガーを無効にする
        }
    }
}
