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

    [SerializeField] private float coolTime = 0.2f;
    [SerializeField] private float existTime = 10.0f;
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private GameObject explosion;
    [SerializeField] private Material[] materials = new Material[2];
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
        _renderer = GetComponent<Renderer>();
        if(_renderer != null && materials[0] != null)
        {
            _renderer.material = materials[0];
        }
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
                if (_renderer != null && materials[1] != null)
                {
                    _renderer.material = materials[1];
                }
                state = State.WithPlayer;
            }
        }
        else if(state == State.WithPlayer)
        {
            transform.forward = (target.transform.position - transform.position).normalized;
#if true
            if (coolTimer < 0.0f)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    coolTimer = coolTime;
                    GameObject gameObject = Instantiate(bullet, transform.position, Quaternion.identity, route);
                    gameObject.GetComponent<BulletMove>().Init(target.transform, player);
                    gameObject.transform.forward = 
                        (target.transform.localPosition - (transform.parent.localPosition + transform.parent.parent.parent.transform.localPosition)).normalized;
                }
                if (Input.GetKeyDown(KeyCode.X))
                {
                    coolTimer = coolTime;
                    GameObject gameObject = Instantiate(bullet, transform.position, Quaternion.identity);
                    gameObject.GetComponent<BulletMove>().Init(target.transform, player);
                    gameObject.transform.forward = transform.forward;
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
