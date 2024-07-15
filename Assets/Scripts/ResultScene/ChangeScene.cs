using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    void Update()
    {
        // XboxコントローラーのBボタンを押したときにTitleSceneに遷移
        if (Input.GetButtonDown("Fire2")) // Fire2はデフォルトでBボタンにマッピングされています
        {
            ToTitleButton();
        }

        // XboxコントローラーのAボタンを押したときにInGameSceneに遷移
        if (Input.GetButtonDown("Fire1")) // Fire1はデフォルトでAボタンにマッピングされています
        {
            Retrybutton();
        }
    }

    public void Retrybutton()
    {
        // 「InGame」Sceneに飛ぶようにしています
        SceneManager.LoadScene("InGameScene");
    }

    public void ToTitleButton()
    {
        SceneManager.LoadScene("TitleScene");
    }
}