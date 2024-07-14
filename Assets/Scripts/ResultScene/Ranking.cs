using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    string[] ranking = new string[5] { "1位", "2位", "3位", "4位", "5位" };
    int[] rankingValue = new int[5] { 0, 0, 0, 0, 0 };

    GameObject dataStocker;

    private void Awake()
    {
        dataStocker = GameObject.Find("DataStocker");
        GetRanking();

        SetRanking(dataStocker.GetComponent<DataStocker>().GetScore());
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    // ランキング呼び出し
    void GetRanking()
    {
        // ランキング呼び出し
        for (int i = 0; i < ranking.Length; i++)
        {
            rankingValue[i] = PlayerPrefs.GetInt(ranking[i], 0); // デフォルト値を0に設定
        }
    }

    // ランキング書き込み
    void SetRanking(int _value)
    {
        // 書き込み用
        for (int i = 0; i < ranking.Length; i++)
        {
            // 取得した値とRankingの値を比較して入れ替え
            if (_value > rankingValue[i])
            {
                var change = rankingValue[i];
                rankingValue[i] = _value;
                _value = change;
            }
        }

        // 入れ替えた値を保存
        for (int i = 0; i < ranking.Length; i++)
        {
            PlayerPrefs.SetInt(ranking[i], rankingValue[i]);
        }
    }

    public int GetScore(int i)
    {
        return rankingValue[i];
    }
}
