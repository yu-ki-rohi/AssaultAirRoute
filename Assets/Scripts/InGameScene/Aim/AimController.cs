using UnityEngine;

public class AimAndShoot : MonoBehaviour
{
    public GameObject target; // ��ʒ��S�ɔz�u������̃Q�[���I�u�W�F�N�g
    public GameObject bulletPrefab; // �e�̃v���n�u
    public Transform bulletSpawnPoint; // �e�̔��ˈʒu
    public float manualMoveSpeed = 5f; // �\���L�[�ł̈ړ����x
    public float autoMoveSpeed = 10f; // �����ړ����x
    public float idleTimeThreshold = 0.1f; // ���͂��Ȃ������Ƃ݂Ȃ����ԁi�b�j
    public float bulletSpeed = 10f; // �e�̑��x

    private float lastInputTime; // �Ō�ɓ��͂�����������

    void Update()
    {
        // �\���L�[�̓��͂��擾
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // ���͂��������ꍇ�A�Ō�̓��͎��Ԃ��X�V
        if (horizontalInput != 0 || verticalInput != 0)
        {
            lastInputTime = Time.time;
            Vector3 inputDirection = new Vector3(horizontalInput, verticalInput, 0).normalized;
            transform.position += inputDirection * manualMoveSpeed * Time.deltaTime;
        }
        else if (Time.time - lastInputTime >= idleTimeThreshold)
        {
            // ��莞�ԓ��͂��Ȃ������ꍇ�A���炩�Ƀ^�[�Q�b�g�Ɍ������Ĉړ�
            Vector3 targetPosition = target.transform.position;
            transform.position = Vector3.Lerp(transform.position, targetPosition, autoMoveSpeed * Time.deltaTime);
        }

        // �X�y�[�X�L�[�������ꂽ�ꍇ�A�e�𔭎�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // �e�̃v���n�u�𐶐�
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // �e�ɑO���ւ̗͂�������
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = bulletSpawnPoint.forward * bulletSpeed;
        }
    }

    void Start()
    {
        // �X�^�[�g���ɍŌ�̓��͎��Ԃ����݂̎��Ԃɐݒ�
        lastInputTime = Time.time;
    }
}
