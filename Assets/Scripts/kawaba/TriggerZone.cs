using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public float buffer = 0.1f;  // �J�����̒[����̃o�b�t�@����
    public float smoothTime = 0.2f;  // �X���C�h���鎞��
    public float distanceFromCamera = 5.0f;  // �J��������̋���
    public float verticalMovementAmplitude = 0.5f;  // �㉺�^���̐U��
    public float verticalMovementSpeed = 1.0f;  // �㉺�^���̑��x

    private Camera mainCamera;
    private bool isActive = false;  // ������L���ɂ���t���O
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (isActive)
        {
            Vector3 screenPos = mainCamera.WorldToViewportPoint(transform.position);

            if (screenPos.x < buffer)
            {
                screenPos.x = buffer;
            }
            else if (screenPos.x > 1 - buffer)
            {
                screenPos.x = 1 - buffer;
            }

            if (screenPos.y < buffer)
            {
                screenPos.y = buffer;
            }
            else if (screenPos.y > 1 - buffer)
            {
                screenPos.y = 1 - buffer;
            }

            // �J�����̑O�Ɉʒu��ݒ�
            screenPos.z = distanceFromCamera;

            targetPosition = mainCamera.ViewportToWorldPoint(screenPos);

            // �㉺�^����ǉ�
            targetPosition.y += Mathf.Sin(Time.time * verticalMovementSpeed) * verticalMovementAmplitude;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }

    public void ActivateKeepInView()
    {
        isActive = true;
    }

    public void DeactivateKeepInView()
    {
        isActive = false;
    }
}
