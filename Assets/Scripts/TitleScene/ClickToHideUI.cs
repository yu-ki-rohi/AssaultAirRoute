using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class ClickToHideAndSlideUI : MonoBehaviour
{
    GraphicRaycaster raycaster;
    EventSystem eventSystem;
    public float moveDistance = 100f; // ���Ɉړ����鋗��
    public float slideDuration = 0.5f; // �X���C�h�̎���
    public TextMeshProUGUI textTitle;

    void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = FindObjectOfType<EventSystem>();

        if (eventSystem == null)
        {
            Debug.LogError("EventSystem��������܂���B�V�[����EventSystem�I�u�W�F�N�g��ǉ����Ă��������B");
        }
    }

    void Update()
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

                    // �c���UI�I�u�W�F�N�g�����ɃX���C�h������
                    StartCoroutine(SlideUIElements());
                    break;
                }
            }
        }
    }

    IEnumerator SlideUIElements()
    {
        GameObject[] uiElements = GameObject.FindGameObjectsWithTag("UI");
        float elapsedTime = 0f;

        // �X�^�[�g�ʒu��ۑ�
        Dictionary<GameObject, Vector2> startPositions = new Dictionary<GameObject, Vector2>();
        foreach (GameObject uiElement in uiElements)
        {
            if (uiElement.activeSelf)
            {
                RectTransform rectTransform = uiElement.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    startPositions[uiElement] = rectTransform.anchoredPosition;
                }
            }
        }

        while (elapsedTime < slideDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / slideDuration);

            foreach (GameObject uiElement in uiElements)
            {
                if (uiElement.activeSelf)
                {
                    RectTransform rectTransform = uiElement.GetComponent<RectTransform>();
                    if (rectTransform != null)
                    {
                        Vector2 startPosition = startPositions[uiElement];
                        rectTransform.anchoredPosition = Vector2.Lerp(startPosition, startPosition - new Vector2(moveDistance, 0), t);
                    }
                }
            }

            yield return null;
        }

        // �ŏI�ʒu��ݒ�
        foreach (GameObject uiElement in uiElements)
        {
            if (uiElement.activeSelf)
            {
                RectTransform rectTransform = uiElement.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    Vector2 startPosition = startPositions[uiElement];
                    rectTransform.anchoredPosition = startPosition - new Vector2(moveDistance, 0);
                    Debug.Log("Moved " + uiElement.name + " to " + rectTransform.anchoredPosition);
                }
            }
        }
    }
}
