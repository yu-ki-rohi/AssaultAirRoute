using System;

[Serializable]
public class ReferStatus
{
    public enum AttackType
    {
        Main,
        Sub01,
        Sub02,
        Special
    }

    public AttackType _referAttackType = AttackType.Main;
    private int[] _attacks;

    public void SetAttacks (CharacterBase characterBase)
    {
        _attacks = new int[4]
        {
            characterBase.Atk,
            characterBase.AtkSub01,
            characterBase.AtkSub02,
            characterBase.AtkSpecial
        };
    }

    public int GetAttack(int id)
    {
        if( id < 0 || id >= _attacks.Length )
        {
            return 0;
        }
        return _attacks[id];
    }



}
