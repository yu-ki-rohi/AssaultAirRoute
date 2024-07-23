using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*
        ScenePhase・・・ゲーム中の状態のフェーズ

        EnemyInFront        雑魚戦に入る直前のフェーズ
        EnemyBattle         雑魚戦のフェーズ
        EnemyEnd            雑魚戦終わりのフェーズ
        BossInFront         ボス戦に入る直前のフェーズ
        BossBattle          ボス戦のフェーズ
        BossDestroy         ボス撃破のフェーズ
     */
    public enum  ScenePhase
    {
        EnemyInFront,  
        EnemyBattle,    
        EnemyEnd,       
        BossInFront,
        BossBattle, 
        BossDestroy
    }
    private Animator animator;
    

    [SerializeField] private Transform _playerPos;
    [SerializeField] private Transform _bossPhasePos;

    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _boss;

    [SerializeField] private ScenePhase _currentPhase = ScenePhase.EnemyInFront;

    public static GameManager Instance;

    private const int NEXT = 1;

    private void Awake()
    {
        if (Instance != null)
        {
            // if Instance is already set, destroy this duplicate
            Destroy(gameObject);
        }
        else
        {
            // if Instance is not set, make this instance the singleton
            Instance = this;
        }
        Debug.Log(_currentPhase);
    }

    private void Start()
    {
        animator.SetTrigger("GameStart");
    }

    public void ChangePhase()
    {
        if (_currentPhase == ScenePhase.BossDestroy)
        {
            return;
        }
        _currentPhase = (ScenePhase)((int)_currentPhase + NEXT);
        Debug.Log(_currentPhase);
    }

    public void ChangeBossPhase()
    {
        _enemy.SetActive(false);
        _boss.SetActive(true);
        _playerPos.position = _bossPhasePos.position;
        _playerPos.forward = _bossPhasePos.forward;
    }

    public void WhiteOutCall()
    {
        animator.SetTrigger("WhiteOut");
    }

    public ScenePhase CurrentPhase { get { return _currentPhase; } }


    public Animator EntryAnimator { set { animator = value; } }
}
