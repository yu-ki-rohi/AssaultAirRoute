using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteManager : MonoBehaviour
{
    [SerializeField] private Rail _rail;
    [SerializeField] private float _time = 60;
    private float _timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Time.deltaTime);
        transform.position = _rail.GetPos(_timer / _time);
        _timer += Time.deltaTime;
        if (_timer / _time < 1.0f)
        {
            transform.forward = _rail.GetPos(_timer / _time) - transform.position;
        }
        
    }
}
