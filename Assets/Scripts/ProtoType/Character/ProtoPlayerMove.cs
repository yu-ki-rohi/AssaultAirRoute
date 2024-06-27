using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoPlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float force = 50.0f;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float _horizontallySup = 4.5f;
    [SerializeField] private float _varticalSup = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow) && transform.localPosition.y < _varticalSup)
        {
            input += Vector3.up;
        }
        if(Input.GetKey(KeyCode.DownArrow) && transform.localPosition.y > -_varticalSup)
        {
            input += Vector3.down;
        }
        if(Input.GetKey(KeyCode.LeftArrow) && transform.localPosition.x > -_horizontallySup)
        {
            input += Vector3.left;
        }
        if(Input.GetKey(KeyCode.RightArrow) && transform.localPosition.x < _horizontallySup)
        {
            input += Vector3.right;
        }
#if true
        transform.localPosition += input * speed * Time.deltaTime;
#else
        rigidbody.AddForce(input * force * Time.deltaTime,ForceMode.Acceleration);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0.0f);
#endif
    }
}
