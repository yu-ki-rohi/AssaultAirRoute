using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossBreak : MonoBehaviour
{
    public GameObject objectToBlink;
    public float blinkInterval = 0.5f; // �_�ł̊Ԋu�i�b�j
    public int blinkCount = 5; // �_�ł����
    private int currentBlink = 0;
    private bool isBlinking = false;

    void Update()
    {
        // �X�y�[�X�L�[�������ꂽ��_�ł��J�n
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
        objectToBlink.SetActive(true); // �_�ł��I��������I�u�W�F�N�g��\������  
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
        StopBlink(); // �I�u�W�F�N�g���j�������Ƃ��ɓ_�ł��~����
    }
}
