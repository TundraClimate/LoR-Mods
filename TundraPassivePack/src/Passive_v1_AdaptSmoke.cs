public class PassiveAbility_TundraPassivePack_AdaptSmoke : AdvancedPassiveBase
{
    public override void OnRoundStart()
    {
        var v1 = _consumed / 2;

        if (v1 != 0)
        {
            base.owner?.bufListDetail?.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, v1.Min(2));
            base.owner?.bufListDetail?.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, v1.Min(2));
        }

        var v2 = _consumed / 3;

        if (v2 != 0)
        {
            base.owner?.cardSlotDetail?.RecoverPlayPoint(v2.Min(3));
        }

        var v3 = _consumed / 5;

        if (v3 != 0)
        {
            base.owner?.allyCardDetail?.DrawCards(v3.Min(2));
        }
    }

    public override void OnRoundEnd_before()
    {
        _consumed = base.owner?.GetBufStack<BattleUnitBuf_smoke>() ?? 0;

        base.owner?.RemoveBuf<BattleUnitBuf_smoke>();
    }

    private int _consumed = 0;
}
