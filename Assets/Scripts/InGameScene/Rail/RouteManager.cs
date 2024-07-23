using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteManager : MonoBehaviour
{
    [SerializeField] private Rail[] _rails;
    [SerializeField] private float[] _times;
    private int _id = 0;
    private float _timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(_id < _rails.Length)
        {
            transform.position = _rails[_id].GetPos(_timer / _times[_id % _times.Length]);
            _timer += Time.deltaTime;
            if (_timer / _times[_id] < 1.0f)
            {
                transform.forward = _rails[_id].GetPos(_timer / _times[_id % _times.Length]) - transform.position;
            }
            else
            {
                _id++;
                _timer = 0.0f;
            }
        }
        else
        {
            SceneController.Instance.ChangeBoss();
        }
        
        
    }
}
