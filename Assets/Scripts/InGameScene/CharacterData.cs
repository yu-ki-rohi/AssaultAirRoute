// yu-ki-rohi
// 参考にしたサイト
// https://www.last-dragon.work/unity/gstatus.html#midashi1

using UnityEngine;

[CreateAssetMenu(menuName = "Data/Create StatusData")]
public class CharacterData : ScriptableObject
{
    public string NAME; //キャラ・敵名
    public int MAXHP; //最大HP
    public int ATK; //攻撃力
    public int DEF; //防御力
    public int AGI; //移動速度
}
