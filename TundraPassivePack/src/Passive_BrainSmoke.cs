public class PassiveAbility_TundraPassivePack_BrainSmoke : AdvancedPassiveBase
{
    public override void OnWaveStart()
    {
        base.owner?.GetBufAndInitIfNull<BattleUnitBuf_smoke>(() => new BattleUnitBuf_smoke() { stack = 5 });
    }

    public override void OnRoundStart()
    {
        if (base.owner?.GetBufStack<BattleUnitBuf_smoke>()?.Let(stack => stack >= 7) == true)
        {
            base.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 3, base.owner);
            base.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Quickness, 3, base.owner);
        }
        else
        {
            base.owner?.bufListDetail?.AddKeywordBufThisRoundByEtc(KeywordBuf.Weak, 2, base.owner);
            base.owner?.breakDetail?.TakeBreakDamage(10, DamageType.Passive, base.owner);
        }
    }
}
