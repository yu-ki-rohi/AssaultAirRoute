using UnityEngine;

public class MoveUIToTarget : MonoBehaviour
{
    public RectTransform uiElement; // The UI element to move
    public Vector2 targetPosition;  // The target position
    public float speed = 1.0f; // Speed of the movement
    private BlinkUi Dis;

    private void Start()
    {
        Dis = GetComponent<BlinkUi>();
    }

    private void Update()
    {
        if (Dis != null && Dis.UIBlinkdisappear)
        {
            MoveUI();
        }
    }

    private void MoveUI()
    {
        // Smoothly move the UI element towards the target position
        uiElement.anchoredPosition = Vector2.Lerp(uiElement.anchoredPosition, targetPosition, speed * Time.deltaTime);
    }
}
