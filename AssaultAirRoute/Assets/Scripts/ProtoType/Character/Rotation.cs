using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float speedX = 0.0f;
    [SerializeField] private float speedY = 0.0f;
    [SerializeField] private float speedZ = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(speedX * Time.deltaTime, speedY * Time.deltaTime, speedZ * Time.deltaTime));
    }
}
