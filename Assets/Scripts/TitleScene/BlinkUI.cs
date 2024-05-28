using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BlinkUi : MonoBehaviour
{
    public bool UIBlinkdisappear = false;
    public TextMeshProUGUI tmpText;
    public float blinkDuration = 1.0f;  // �_�ł̊Ԋu
    GraphicRaycaster raycaster;
    EventSystem eventSystem;

    private void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = FindObjectOfType<EventSystem>();
        // �R���[�`�����J�n����
        StartCoroutine(Blink());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(pointerEventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.CompareTag("UI"))
                {
                    // �q�b�g�����I�u�W�F�N�g���\���ɂ���
                    Debug.Log("Clicked on: " + result.gameObject.name);
                    result.gameObject.SetActive(false);
                    UIBlinkdisappear = true;
                }
            }
        }
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            // �A���t�@�l��ύX���ē_�Ō��ʂ��쐬����
            for (float t = 0.01f; t < blinkDuration; t += Time.deltaTime)
            {
                Color color = tmpText.color;
                color.a = Mathf.Lerp(0, 1, Mathf.PingPong(t * 2, 1));
                tmpText.color = color;
                yield return null;
            }
        }
    }
}


