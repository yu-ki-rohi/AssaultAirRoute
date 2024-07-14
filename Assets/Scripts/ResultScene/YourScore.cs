using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YourScore : MonoBehaviour
{
    GameObject dataStocker;
    Text scoreText;
    int yourscore;
    // Start is called before the first frame update
    void Start()
    {
        dataStocker = GameObject.Find("DataStocker");
        scoreText = GetComponentInChildren<Text>();
        yourscore = dataStocker.GetComponent<DataStocker>().GetScore();
        scoreText.text = yourscore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
