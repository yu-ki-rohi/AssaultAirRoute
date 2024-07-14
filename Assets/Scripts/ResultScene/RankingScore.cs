using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingScore : MonoBehaviour
{
    [SerializeField]
    int rank;

    GameObject ranking;
    Text scoreText;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        ranking = GameObject.Find("Ranking");
        score = ranking.GetComponent<Ranking>().GetScore(rank);
        scoreText = GetComponentInChildren<Text>();
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
