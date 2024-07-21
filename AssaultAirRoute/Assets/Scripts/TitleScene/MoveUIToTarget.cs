using UnityEngine;

public class MoveUIToTarget : MonoBehaviour
{
    public RectTransform uiElement; // 移動するUI要素
    public Vector2 targetPosition;  // 目標位置
    public float speed = 1.0f; // 移動速度
    public float fadeOutSpeed = 10.0f; // フェードアウト速度
    private CanvasGroup canvasGroup; // 透明度を管理するCanvasGroup
    private BlinkUI Dis; // BlinkUIスクリプトの参照
    public bool UIFade = false;

    private void Start()
    {
        Dis = GetComponent<BlinkUI>();
        canvasGroup = uiElement.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("UI要素にCanvasGroupコンポーネントがありません。");
        }
    }

    private void Update()
    {
        if (Dis != null && Dis.UIBlinkdisappear == true)
        {
            MoveUI();
            FadeOutUI();
        }
    }

    private void MoveUI()
    {
        // UI要素を目標位置に滑らかに移動
        uiElement.anchoredPosition = Vector2.Lerp(uiElement.anchoredPosition, targetPosition, speed * Time.deltaTime);
    }

    private void FadeOutUI()
    {
        if (canvasGroup != null)
        {
            // フェードアウトするためにアルファ値を徐々に減少させる
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0, fadeOutSpeed * Time.deltaTime);
            // 完全に透明になったらUI要素を非アクティブにする
            if (canvasGroup.alpha <= 0.01f)
            {
                canvasGroup.alpha = 0;
                uiElement.gameObject.SetActive(false);
                UIFade = true;
            }
        }
    }
}