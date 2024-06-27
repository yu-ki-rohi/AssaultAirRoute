using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform target;
    [SerializeField] private Transform route;
    [SerializeField] private Transform firePosition;
    [SerializeField] private float coolTime = 0.1f;

    private float timer = 0.0f;

    [SerializeField] private GameObject player;

    [SerializeField] private Transform capturePosParent;
    [SerializeField] private Transform[] capturePos;
    // Start is called before the first frame update
    void Start()
    {
        capturePos = new Transform[capturePosParent.childCount];
        for (int i = 0; i < capturePosParent.childCount; i++)
        {
            capturePos[i] = capturePosParent.GetChild(i).transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //transform.forward = Vector3.Cross(transform.up, (target.transform.position - transform.position).normalized);
        //transform.right = (-target.transform.position + transform.position).normalized;
        //transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        //transform.up = transform.parent.up;
        transform.LookAt(transform.position +
            Vector3.Cross(transform.up, (target.transform.position - transform.position).normalized),
            Vector3.Cross(
                Vector3.Cross(transform.parent.up, (target.transform.position - transform.position).normalized),
                -(target.transform.position - transform.position).normalized));

#if true
        if (timer <= 0.0f)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                timer = coolTime;
                GameObject gameObject = Instantiate(bullet, firePosition.position, Quaternion.identity, route);
                gameObject.GetComponent<BulletMove>().Init(target, player);
                gameObject.transform.forward = 
                    (target.localPosition - transform.localPosition).normalized;
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                timer = coolTime;
                GameObject gameObject = Instantiate(bullet, firePosition.position, Quaternion.identity);
                gameObject.GetComponent<BulletMove>().Init(target, player);
                gameObject.transform.forward = -transform.right;
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
       
#endif
    }

    public void CaptureEnemy(Transform spawnTransform_, GameObject capturedEnemy)
    {
        for(int i = 0; i < capturePos.Length; i++)
        {
            if (capturePos[i].transform.childCount <= 0)
            {
                GameObject cap =Instantiate(capturedEnemy, spawnTransform_.position, Quaternion.identity, capturePos[i]);
                cap.GetComponent<CapturedEnemy>().Init(bullet, target, route, gameObject, capturePos[i].transform);
                return;
            }
        }
    }

}
