using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossBreak : MonoBehaviour
{
    public GameObject objectToBlink;
    public float blinkInterval = 0.5f; // 点滅の間隔（秒）
    public int blinkCount = 5; // 点滅する回数
    private int currentBlink = 0;
    private bool isBlinking = false;

    void Update()
    {
        // スペースキーが押されたら点滅を開始
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isBlinking)
            {
                StartBlink();
            }
        }
    }

    void StartBlink()
    {
        isBlinking = true;
        currentBlink = 0;
        InvokeRepeating("ToggleVisibility", 0f, blinkInterval);
    }

    void StopBlink()
    {
        isBlinking = false;
        CancelInvoke("ToggleVisibility");
        objectToBlink.SetActive(true); // 点滅が終了したらオブジェクトを表示する  
    }

    void ToggleVisibility()
    {
        objectToBlink.SetActive(!objectToBlink.activeSelf);
        currentBlink++;

        if (currentBlink >= blinkCount)
        {
            StopBlink();
        }
    }

    void OnDestroy()
    {
        StopBlink(); // オブジェクトが破棄されるときに点滅を停止する
    }
}
