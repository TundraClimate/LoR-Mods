public class PassiveAbility_TundraPassivePack_Psycho : AdvancedPassiveBase
{
    public override void OnWaveStart()
    {
        _breaked = false;
    }

    public override void OnRoundStart()
    {
        if (1 > _count)
        {
            return;
        }

        base.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Protection, _count, base.owner);

        if (_breaked)
        {
            base.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, _count, base.owner);
            base.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.DmgUp, _count, base.owner);
            base.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Vulnerable, _count * 2, base.owner);
        }
    }

    public override bool OnBreakGageZero()
    {
        var units = base.owner.faction.AliveUnits;

        if (!_breaked && units.Count == 1 && units[0] == base.owner)
        {
            _breaked = true;

            base.owner.breakDetail.ResetGauge();

            base.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, _count, base.owner);
            base.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.DmgUp, _count, base.owner);
            base.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Vulnerable, _count * 2, base.owner);

            return true;
        }

        return false;
    }

    public override bool TeamKill()
    {
        return true;
    }

    public override void OnKill(BattleUnitModel target)
    {
        if (target.faction != base.owner.faction)
        {
            return;
        }

        base.owner.TakeBreakDamage((int)(base.owner.breakDetail.GetDefaultBreakGauge() * 0.3), DamageType.Passive, base.owner);

        base.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Protection, 1, base.owner);

        _count += 1;
    }

    private int _count;

    private bool _breaked;
}
