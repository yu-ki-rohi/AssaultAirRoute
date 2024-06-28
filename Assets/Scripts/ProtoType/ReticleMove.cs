using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform center;
    [SerializeField] private float _horizontallySup;
    [SerializeField] private float _varticalSup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow) && transform.localPosition.y < _varticalSup)
        {
            input += Vector3.up;
        }
        if (Input.GetKey(KeyCode.DownArrow) && transform.localPosition.y > -_varticalSup)
        {
            input += Vector3.down;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && transform.localPosition.x > -_horizontallySup)
        {
            input += Vector3.left;
        }
        if (Input.GetKey(KeyCode.RightArrow) && transform.localPosition.x < _horizontallySup)
        {
            input += Vector3.right;
        }

        if(!Input.GetKey(KeyCode.UpArrow) &&
            !Input.GetKey(KeyCode.DownArrow) &&
            !Input.GetKey(KeyCode.LeftArrow) &&
            !Input.GetKey(KeyCode.RightArrow))
        {
            input = (center.transform.localPosition - transform.localPosition) * 0.3f;
        }

        transform.localPosition += input * speed * Time.deltaTime;

    }
}
