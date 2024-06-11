// yu-ki-rohi
// 参考にしたサイト
// https://www.last-dragon.work/unity/gstatus.html#midashi1

using UnityEngine;

[CreateAssetMenu(menuName = "Data/Create StatusData")]
public class CharacterData : ScriptableObject
{
    [HideInInspector] public int MAXHP = 100;        // 最大HP
    [HideInInspector] public int ATK = 10;           // メイン攻撃力
    [HideInInspector] public int ATK_SUB1 = 0;       // サブ攻撃力1
    [HideInInspector] public int ATK_SUB2 = 0;       // サブ攻撃力2
    [HideInInspector] public int ATK_SPECIAL = 0;    // 必殺技攻撃力
    [HideInInspector] public float AGI = 5.0f;       // 移動速度
    [HideInInspector] public int BOUNTY = 0;         // 金
    [HideInInspector] public float COOLTIME = 0.5f;  // 次の攻撃までの時間

    [SerializeField] private int _maxHP = 100;
    [SerializeField] private int _atk = 10;
    [SerializeField] private int _atkSub1 = 0;
    [SerializeField] private int _atkSub2 = 0;
    [SerializeField] private int _atkSpecial = 0;
    [SerializeField] private float _agi = 5.0f;
    [SerializeField] private int _bounty = 0;
    [SerializeField] private float _coolTime = 0.5f;


    public void Initialize()
    {
        MAXHP = _maxHP;
        ATK = _atk;
        ATK_SUB1 = _atkSub1;
        ATK_SUB2 = _atkSub2;
        ATK_SPECIAL = _atkSpecial;
        BOUNTY = _bounty;
        COOLTIME = _coolTime;
    }
}
