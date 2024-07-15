using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneMan : MonoBehaviour
{

    private Bullet BUl;

    public void Update()
    {
        // Enterキーが押されたら
        if (BUl.SceneCange == true)
        {
            // シーンをロードする
            SceneManager.LoadScene("InGameScene");
        }
    }
}