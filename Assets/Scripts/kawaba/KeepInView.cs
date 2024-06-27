using UnityEngine;

// ����GameObject���ɓ��邱�Ƃ�

public class KeepInView : MonoBehaviour
{
    public float buffer = 0.1f;  // �J�����̒[����̃o�b�t�@����
    public float smoothTime = 0.2f;  // �X���C�h���鎞��
    public float distanceFromPlayer = 5.0f;  // �v���C���[����̋���
    public float verticalMovementAmplitude = 0.5f;  // �㉺�^���̐U��
    public float verticalMovementSpeed = 1.0f;  // �㉺�^���̑��x

    [SerializeField] private float _up = 0.0f;
    [SerializeField] private float _right = 0.0f;

    private Camera mainCamera;
    // @yu-ki-rohi
    // player��Transform��񂵂��g���ĂȂ��̂ŁA
    // GameObject�܂ł͕K�v�Ȃ��悤�ȋC�����܂���
    // �����łǂꂭ�炢�����o��̂��͕�����Ȃ��ł����A
    // ���̃X�N���v�g�ł͓����悤�ȂƂ���ŁA
    // Transform�ɂ��Ă����̂ŋC�ɂȂ�܂���
    private GameObject player;
    private bool isActive = false;  // ������L���ɂ���t���O
    private Vector3 targetPosition = Vector3.zero;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        mainCamera = Camera.main;

        // @yu-ki-rohi
        // �����Start���̏����Ȃ̂ŁA
        // �����܂Ŗڂ����痧�Ă�قǂ̂��Ƃł��Ȃ��ł���
        // FindWithTag�͏d�߂̏����Ȃ̂łȂ�ׂ��������ق����ǂ��ł�
        // ���ɍ����Active������������̂ł����Ŏ󂯓n�����ł���Ǝv���܂�
#if false
        player = GameObject.FindWithTag("Player");
#endif
    }

    void LateUpdate()
    {
        if (isActive)
        {
#if true
            // �v���C���[�̈ʒu�Ɋ�Â��ă^�[�Q�b�g�I�u�W�F�N�g�̖ڕW�ʒu���v�Z
            if (player != null)
            {
                targetPosition = player.transform.position + player.transform.forward * distanceFromPlayer;
                targetPosition += player.transform.up * _up;
                targetPosition += player.transform.right * _right;
            }

#else
            // �v���C���[�̈ʒu�Ɋ�Â��ă^�[�Q�b�g�I�u�W�F�N�g�̖ڕW�ʒu���v�Z
            Vector3 offset = (transform.position - player.transform.position).normalized * distanceFromPlayer;
            Vector3 desiredPosition = player.transform.position + offset;

            // �J�����̕`��͈͓��Ƀ^�[�Q�b�g�I�u�W�F�N�g��z�u���邽�߂̃X�N���[�����W���v�Z
            Vector3 screenPos = mainCamera.WorldToViewportPoint(desiredPosition);

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

            // �V�����^�[�Q�b�g�|�W�V�������v�Z
            targetPosition = mainCamera.ViewportToWorldPoint(screenPos);

            // �㉺�^����ǉ�
            targetPosition.y += Mathf.Sin(Time.time * verticalMovementSpeed) * verticalMovementAmplitude;

#endif

            // �X���[�Y�Ƀ^�[�Q�b�g�|�W�V�����ֈړ�
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }

    public void ActivateKeepInView(GameObject target)
    {
        player = target;
        isActive = true;
    }

    public void DeactivateKeepInView()
    {
        isActive = false;
    }
}
