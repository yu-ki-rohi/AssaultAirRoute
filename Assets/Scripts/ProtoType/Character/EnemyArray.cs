using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArray : MonoBehaviour
{
    private GameObject[] _enemys;
    public GameObject[] Enemys { get { return _enemys; } }
    private void Awake()
    {
        _enemys = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            _enemys[i] = transform.GetChild(i).gameObject;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
