using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject captureBullet;
    [SerializeField] private CharacterBase characterBase;
    [SerializeField] private Transform target;
    [SerializeField] private Transform route;
    [SerializeField] private Transform firePosition;
    [SerializeField] private float firePositionAdjustment = 1.0f;
    private float coolTime = 0.1f;
    [SerializeField, Range(0.0f,0.3f)] private float verticalRange = 0.2f;
    [SerializeField, Range(0.0f, 0.3f)] private float horizontallyRange = 0.2f;

    private float timer = 0.0f;

    [SerializeField] private GameObject player;

    [SerializeField] private Transform capturePosParent;
    [SerializeField] private Transform[] capturePos;
    [SerializeField] private BulletSetting _bulletSetting = new BulletSetting();
    [SerializeField] private BulletSetting _captureBulletSetting = new BulletSetting();

    public Transform Route { get { return route; } }

    
    // Start is called before the first frame update
    void Start()
    {
        capturePos = new Transform[capturePosParent.childCount];

        for (int i = 0; i < capturePosParent.childCount; i++)
        {
            capturePos[i] = capturePosParent.GetChild(i).transform;
        }

        if (characterBase != null)
        {
            coolTime = characterBase.CoolTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position +
            Vector3.Cross(transform.parent.up, (target.transform.position - transform.position).normalized),
            Vector3.Cross(
                Vector3.Cross(transform.parent.up, (target.transform.position - transform.position).normalized),
                -(target.transform.position - transform.position).normalized));

#if true
        if (timer <= 0.0f)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if(characterBase != null)
                {
                    timer = coolTime;
                    Vector3 adjustment = (target.transform.position - firePosition.position).normalized * firePositionAdjustment;
                    GameObject gameObject = Instantiate(bullet, firePosition.position + adjustment, Quaternion.identity, route);
                    BulletMove bulletMove = gameObject.AddComponent<BulletMove>();
                    bulletMove.Init(characterBase.Atk, player, route);
                    if(_bulletSetting.useSetting)
                    {
                        bulletMove.Setting(_bulletSetting.speed, _bulletSetting.existTime);
                    }
                    gameObject.transform.forward = -transform.right;
                    if(characterBase.AtkSub01 > 0)
                    {                      
                        for (int i = -1; i < 2; i += 2)
                        {
                            GameObject subGameObject = Instantiate(bullet, firePosition.position + adjustment, Quaternion.identity, route);
                            BulletMove subBulletMove = subGameObject.AddComponent<BulletMove>();
                            subBulletMove.Init(characterBase.AtkSub01, player, route);
                            if (_bulletSetting.useSetting)
                            {
                                subBulletMove.Setting(_bulletSetting.speed, _bulletSetting.existTime);
                            }
                            subGameObject.transform.forward = (-transform.right + transform.up * verticalRange * i).normalized;
                        }
                    }
                    if (characterBase.AtkSub02 > 0)
                    {
                        for (int i = -1; i < 2; i += 2)
                        {
                            GameObject subGameObject = Instantiate(bullet, firePosition.position + adjustment, Quaternion.identity, route);
                            BulletMove subBulletMove = subGameObject.AddComponent<BulletMove>();
                            subBulletMove.Init(characterBase.AtkSub02, player, route);
                            if (_bulletSetting.useSetting)
                            {
                                subBulletMove.Setting(_bulletSetting.speed, _bulletSetting.existTime);
                            }
                            subGameObject.transform.forward = (-transform.right + transform.forward * horizontallyRange * i).normalized;
                        }
                    }
                }
                
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (characterBase != null)
                {
                    timer = coolTime;
                    Vector3 adjustment = (target.transform.position - firePosition.position).normalized * firePositionAdjustment;
                    GameObject gameObject = Instantiate(captureBullet, firePosition.position + adjustment, Quaternion.identity, route);
                    BulletMove bulletMove = gameObject.AddComponent<BulletMove>();
                    bulletMove.Init(characterBase.Atk / 2, player, route, true);
                    if (_captureBulletSetting.useSetting)
                    {
                        bulletMove.Setting(_captureBulletSetting.speed, _captureBulletSetting.existTime);
                    }
                    gameObject.transform.forward = -transform.right;
                }
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
