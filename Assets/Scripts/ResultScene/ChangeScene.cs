using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
  public void Retrybutton()
    {
        //仮に「InGame」Sceneに飛ぶようにしています
        SceneManager.LoadScene("InGameScene");
    }

    public void ToTitleButton()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
