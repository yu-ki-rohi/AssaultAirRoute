using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteManager : MonoBehaviour
{
    [SerializeField] private Rail _rail;
    [SerializeField, Range(0, 1)] private float speed = 0.01f;
    private float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _rail.GetPos(time);
        time += speed;
        transform.forward = _rail.GetPos(time) - transform.position;
    }
}
