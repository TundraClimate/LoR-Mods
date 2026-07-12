public class PassiveAbility_TundraPassivePack_ChargeToPow : AdvancedPassiveBase
{
    public override void OnRoundStart()
    {
        var consumes = _consumed;

        _consumed = 0;

        if (consumes >= 10)
        {
            base.owner?.bufListDetail?.AddKeywordBufByEtc(KeywordBuf.DmgUp, 3, base.owner);
        }

        if (consumes >= 20)
        {
            base.owner?.bufListDetail?.AddKeywordBufByEtc(KeywordBuf.Protection, 2, base.owner);
            base.owner?.bufListDetail?.AddKeywordBufByEtc(KeywordBuf.BreakProtection, 8, base.owner);
        }

        if (consumes >= 30)
        {
            base.owner?.bufListDetail?.AddKeywordBufByEtc(KeywordBuf.Strength, 2, base.owner);
            base.owner?.bufListDetail?.AddKeywordBufByEtc(KeywordBuf.Endurance, 5, base.owner);
        }

        if (consumes >= 40)
        {
            base.owner?.bufListDetail?.AddKeywordBufByEtc(KeywordBuf.Protection, 6, base.owner);
            base.owner?.bufListDetail?.AddKeywordBufByEtc(KeywordBuf.BreakProtection, 12, base.owner);
        }

        if (consumes >= 50)
        {
            base.owner?.bufListDetail?.AddKeywordBufByEtc(KeywordBuf.Strength, 3, base.owner);
            base.owner?.bufListDetail?.AddKeywordBufByEtc(KeywordBuf.Endurance, 7, base.owner);
        }
    }

    public override void OnChangeBufStack(BattleUnitBuf changed, int last)
    {
        if (last > changed.stack && changed is BattleUnitBuf_warpCharge)
        {
            _consumed += last - changed.stack;
        }
    }

    private int _consumed;
}
