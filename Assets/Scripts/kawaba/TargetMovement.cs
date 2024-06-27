using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TargetMovement : MonoBehaviour
{
    // @yu-ki-rohi
    // player��Transform�擾�A����������t���Ȃ��Ă��A
    // DetectCollide�p�ӂ���Player�ɓ���������
    // ���̎��Ɏ擾�Ƃ��ɂ���Ƃ�������
    public Transform player; // �v���C���[��Transform
    public float distanceFromPlayer = 10f; // �v���C���[����̋���
    public float movementSpeed = 2f; // �^�[�Q�b�g�̈ړ����x
    public float movementRange = 5f; // �^�[�Q�b�g�̈ړ��͈�
    public float minDistanceBetweenTargets = 3f; // �^�[�Q�b�g���m�̍ŏ�����
    public float maxMovementChangeInterval = 3f; // �ړ��ύX�̍ő�Ԋu
    public float homingDuration = 5f; // �z�[�~���O�̎�������
    public float homingSpeed = 5f; // �z�[�~���O���̑��x

    private Vector3 targetPosition;
    private Vector3 movementDirection;
    private float lastMovementChangeTime;
    private bool isHoming = false;
    [SerializeField]
    private float homingStartTime;

    void Start()
    {
        // �^�[�Q�b�g�̏����ʒu��ݒ�
        SetRandomTargetPosition();
        lastMovementChangeTime = Time.time;

        Invoke("StartHoming", homingStartTime);
    }

    void Update()
    {
        if (!isHoming)
        {
            // ���ꂷ���player�̏㉺���E�̓����ɍ��킹�ē����Ă��܂�����A
            // ������Ƃ܂�������
            // ������������GameObject���ɓ��邱�ƂŁA
            // ���΍��W�ōl�����邩��A
            // �O���Ɉʒu�������邽�߂̏����͕K�v�Ȃ��Ǝv��
            // ����Ɋւ��Ă͐����s���Ő\����Ȃ�


            // �v���C���[�̑O���Ɉʒu������
            Vector3 forwardPosition = player.position + player.forward * distanceFromPlayer;
            transform.position = Vector3.Lerp(transform.position, forwardPosition, Time.deltaTime * movementSpeed);

            // �^�[�Q�b�g���m�̋�����ۂ�
            KeepDistanceBetweenTargets();

            // ���Ԋu�Ń����_���ȕ����Ɉړ�����
            if (Time.time - lastMovementChangeTime > maxMovementChangeInterval)
            {
                lastMovementChangeTime = Time.time;
                SetRandomMovementDirection();
            }

            // �����_���ȕ����Ɉړ�
            transform.position += movementDirection * movementSpeed * Time.deltaTime;
        }
        else
        {
            // �z�[�~���O���̓v���C���[�Ɍ������Ĉړ�
            Vector3 playerDirection = player.position - transform.position;
            transform.position += playerDirection.normalized * homingSpeed * Time.deltaTime;

            // �z�[�~���O�̎������Ԃ��o�߂�����z�[�~���O���I��
            if (Time.time - homingStartTime > homingDuration)
            {
                isHoming = false;
                SetRandomTargetPosition(); // �z�[�~���O���I�������烉���_���Ȉʒu�Ɉړ�
            }
        }

    }

    // �z�[�~���O���J�n���郁�\�b�h
    public void StartHoming()
    {
        isHoming = true;
        homingStartTime = Time.time;
    }

    void SetRandomTargetPosition()
    {
        float randomX = Random.Range(-movementRange, movementRange);
        float randomY = Random.Range(-movementRange, movementRange);
        float randomZ = Random.Range(-movementRange, movementRange);

        targetPosition = player.position + player.forward * distanceFromPlayer + new Vector3(randomX, randomY, randomZ);
        SetRandomMovementDirection();
    }

    void SetRandomMovementDirection()
    {
        movementDirection = Random.insideUnitSphere.normalized;
    }

    void KeepDistanceBetweenTargets()
    {
        // @yu-ki-rohi
        // ���̏���(FindGameObjectsWithTag)�͌��\�d�����炵������A
        // Update�ŌĂяo���͔̂������ق����ǂ�����
        // �΍�Ƃ��čl������̂́A
        // 1. �ŏ�����ЂƂ܂Ƃ܂�Ƃ��ėp��
        // 2. DetectCollider��p�ӂ��ē����������ǉ�
        // �����肩��
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");

        foreach (GameObject otherTarget in targets)
        {
            if (otherTarget != gameObject) // �������g�ȊO�̃^�[�Q�b�g�ɑ΂��Ă̂ݏ���
            {
                float distance = Vector3.Distance(transform.position, otherTarget.transform.position);
                if (distance < minDistanceBetweenTargets)
                {
                    Vector3 direction = transform.position - otherTarget.transform.position;
                    direction.Normalize();
                    transform.position += direction * (minDistanceBetweenTargets - distance) * Time.deltaTime;
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {  
        // �Փ˂����I�u�W�F�N�g���قȂ�ꍇ�ɃX�R�A�𑝉�
        if (collision.gameObject.CompareTag("Bullet"))
        {
            //ScoreManager.Instance.AddScore(GameConstants.Instance.Score);
            Destroy(gameObject);
        }
    }
}