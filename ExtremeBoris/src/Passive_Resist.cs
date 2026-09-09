public class PassiveAbility_ExtremeBoris_Resist : AdvancedPassiveBase
{
    public override int GetDamageReductionAll()
    {
        return 1;
    }

    public override int GetBreakDamageReductionAll(int dmg, DamageType dmgType, BattleUnitModel attacker)
    {
        return 1;
    }

    public override void OnRoundStartFirst()
    {
        var ratio = base.owner.hp / (float)base.owner.MaxHp;
        var num = 10.Min((int)(ratio / 0.1));

        base.owner.bufListDetail.AddKeywordBufThisRoundByEtc(LimKeywordBuf.Poise, num, base.owner);
    }
}
