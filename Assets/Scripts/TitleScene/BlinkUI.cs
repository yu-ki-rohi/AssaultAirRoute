using UnityEngine;
using TMPro;
using System.Collections;

public class BlinkUi : MonoBehaviour
{
    public TextMeshProUGUI tmpText;
    public float blinkDuration = 1.0f;  // �_�ł̊Ԋu

    void Start()
    {
        // �R���[�`�����J�n����
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
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

