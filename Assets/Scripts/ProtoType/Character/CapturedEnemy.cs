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
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject[] figure = new GameObject[2];
    private Renderer _renderer;
    private GameObject bullet;
    private Transform target;
    private Transform route;
    private State state = State.MoveToPlayer;
    private float coolTimer = 0.0f;
    private float existTimer = 0.0f;
    private GameObject player;
    private Rigidbody playerRigitBody;
    private Transform parentTransform;
    // Start is called before the first frame update
    void Start()
    {
        if(player != null)
        {
            playerRigitBody = player.GetComponent<Rigidbody>();
        }
        figure[1].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.MoveToPlayer)
        {
            transform.forward = (parentTransform.position - transform.position).normalized;
            transform.Translate(transform.forward * Time.deltaTime * moveSpeed);

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
            transform.LookAt(target.position, transform.parent.parent.parent.up);
#if true
            if (coolTimer < 0.0f)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    coolTimer = coolTime;
                    Vector3 adjustment = (target.transform.position - transform.position).normalized;
                    GameObject gameObject = Instantiate(bullet, transform.position + adjustment, Quaternion.identity, route);
                    gameObject.GetComponent<BulletMove>().Init(route, null, attack);
                    gameObject.transform.forward = -transform.right;
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
        bullet = bullet_;
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
