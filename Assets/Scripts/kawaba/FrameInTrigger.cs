using UnityEngine;

public class FrameInTrigger : MonoBehaviour
{
    public GameObject[] targetObjects;  // �t���[���C�����������I�u�W�F�N�g�̔z��

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
            gameObject.SetActive(false);  // �g���K�[�𖳌��ɂ���
        }
    }
}
