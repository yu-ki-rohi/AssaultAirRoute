using TMPro;
using UnityEngine;

public class FadeInUI : MonoBehaviour
{
    public RectTransform uiElement; // スライド・フェードインさせるUI要素
    public float slideSpeed = 100.0f; // スライド速度
    public float fadeInSpeed = 10.0f; // フェードイン速度
    public Vector2 targetPosition; // 目標位置（画面内の左側）
    public Vector2 initialPosition; // 初期位置
    private CanvasGroup canvasGroup; // 透明度を管理するCanvasGroup
    [SerializeField]
    private MoveUIToTarget UF;
    public bool TitleSel = false;

    private void Start()
    {
        canvasGroup = uiElement.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("UI要素にCanvasGroupコンポーネントがありません。");
        }

        // 初期位置を設定
        uiElement.anchoredPosition = initialPosition;

        // 初期状態では透明にする
        canvasGroup.alpha = 0;
    }

    private void Update()
    {
        if (UF.UIFade == true)
        {
            float distanceToTarget = Vector2.Distance(uiElement.anchoredPosition, targetPosition);
            float adjustedSlideSpeed = Mathf.Lerp(0, slideSpeed, distanceToTarget / Vector2.Distance(initialPosition, targetPosition));

            // UI要素を目標位置にスライドさせる
            uiElement.anchoredPosition = Vector2.MoveTowards(uiElement.anchoredPosition, targetPosition, adjustedSlideSpeed * Time.deltaTime);

            // フェードイン処理
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, 1.0f, fadeInSpeed * Time.deltaTime);
            TitleSel = true;

            // フェードインが完了したら、移動フラグを立てる
            if (canvasGroup.alpha >= 0.99f && uiElement.anchoredPosition == targetPosition)
            {
                canvasGroup.alpha = 1.0f;
            }
        }
    }
    
}
