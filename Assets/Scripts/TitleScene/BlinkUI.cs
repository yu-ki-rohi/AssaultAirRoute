using UnityEngine;
using TMPro;
using System.Collections;

public class BlinkUi : MonoBehaviour
{
    public TextMeshProUGUI tmpText;
    public float blinkDuration = 1.0f;  // 点滅の間隔

    void Start()
    {
        // コルーチンを開始する
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
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

