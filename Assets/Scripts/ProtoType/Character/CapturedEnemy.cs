using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturedEnemy : MonoBehaviour
{
    enum State
    {
        MoveToPlayer,
        WithPlayer
    }

    [SerializeField] private int attack = 5;
    [SerializeField] private float coolTime = 0.2f;
    [SerializeField] private float existTime = 10.0f;
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField,Range(0.0f,0.1f)] private float diff = 0.5f;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject[] figure = new GameObject[2];
    [SerializeField] private GameObject bullet;
    [SerializeField] private bool isUpFixd = false;
    private Transform target;
    private Transform route;
    private State state = State.MoveToPlayer;
    private float coolTimer = 0.0f;
    private float existTimer = 0.0f;
    private GameObject player;
    private Rigidbody playerRigitBody;
    private Transform parentTransform;
    private Transform handTransform;
    // Start is called before the first frame update
    void Start()
    {
        if(player != null)
        {
            playerRigitBody = player.GetComponent<Rigidbody>();
        }
        figure[0].SetActive(true);
        figure[1].SetActive(false);
        handTransform = parentTransform.parent.parent.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.MoveToPlayer)
        {
            transform.LookAt(parentTransform.position);
            //transform.forward = (parentTransform.position - transform.position).normalized;
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);

            float distance = (parentTransform.position - transform.position).magnitude;
            float near = 1.0f + Time.deltaTime * moveSpeed;
            if(playerRigitBody != null )
            {
                near += playerRigitBody.velocity.magnitude;
            }
            if (distance < near)
            {
                transform.localPosition = Vector3.zero;

                figure[0].SetActive(false);
                figure[1].SetActive(true);

                state = State.WithPlayer;
            }
        }
        else if(state == State.WithPlayer)
        {
            if(isUpFixd)
            {
                transform.LookAt(target.position, handTransform.up);
            }
            else
            {
                transform.LookAt(target.position, transform.parent.up);
            }
            
#if true
            if (coolTimer < 0.0f)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    coolTimer = coolTime;
                    Vector3 adjustment = (target.transform.position - transform.position).normalized;
                    GameObject gameObject = Instantiate(bullet, transform.position + adjustment, Quaternion.identity, route);
                    gameObject.GetComponent<BulletMove>().Init(attack, player, route );
                    Vector3 dir = (transform.position - handTransform.position).normalized;
                    dir = (transform.forward + dir * diff).normalized;
                    gameObject.transform.forward = dir;
                }
                if (Input.GetKeyDown(KeyCode.X))
                {
                    
                }
            }
            else
            {
                coolTimer -= Time.deltaTime;
            }

#endif
            existTimer += Time.deltaTime;
            if(existTimer > existTime)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

    }

    public void Init(GameObject bullet_, Transform target_, Transform route_, GameObject player_, Transform parentTransform_)
    {
        if(bullet == null)
        {
            bullet = bullet_;
        }
        target = target_;
        route = route_;
        player = player_;
        parentTransform = parentTransform_;
    }
    private void DisAppear()
    {
        Destroy(gameObject);
    }
}
