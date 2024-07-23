// yu-ki-rohi

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    enum Scene
    {
        Title,
        InGame,
        Result
    }

    [SerializeField] private CharacterData _playerData;
    [SerializeField] private CharacterData[] _enemyDatas;


    private Scene _currentScene = Scene.Title;

    // Sceneを変更するメソッド
    // Title -> InGame -> Result の順で遷移します
    // SceneController.Instance.ChangeScene()
    // で使用してください
    public void ChangeScene()
    {
        switch(_currentScene)
        {
            case Scene.Title:
                _currentScene = Scene.InGame;

                // ゲーム開始前にデータを初期化
                Initializa();

                SceneManager.LoadScene("InGameScene");
                break;
            case Scene.InGame:
                _currentScene = Scene.Result;
                SceneManager.LoadScene("ResultScene");
                break;
            case Scene.Result:
                _currentScene = Scene.Title;
                SceneManager.LoadScene("TitleScene");
                break;
            default:
                break;
        }
    
    }

    public void ReStart()
    {
        if(_currentScene == Scene.InGame)
        {
            // TODO
            // 敵強化処理---
            foreach (CharacterData enemyData in _enemyDatas)
            {
                
            }
            // -------------
            SceneManager.LoadScene("InGameScene");
        }
    }

    public void Initializa()
    {
        _playerData.Initialize();
        foreach (CharacterData enemyData in _enemyDatas)
        {
            enemyData.Initialize();
        }
    }
}
