using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BlinkUI : MonoBehaviour
{
    public bool UIBlinkdisappear = false;
    public TextMeshProUGUI tmpText;
    public float blinkDuration = 1.0f;  // 点滅の間隔
    GraphicRaycaster raycaster;
    EventSystem eventSystem;

    private void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = FindObjectOfType<EventSystem>();
        // コルーチンを開始する
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
                    // ヒットしたオブジェクトを非表示にする
                    Debug.Log("Clicked on: " + result.gameObject.name);
                    result.gameObject.SetActive(false);
                    UIBlinkdisappear = true;
                    Cursor.visible = false; // Hide the cursor
                }
            }
        }
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            // アルファ値を変更して点滅効果を作成する
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
