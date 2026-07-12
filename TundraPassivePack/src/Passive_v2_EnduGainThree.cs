public class PassiveAbility_TundraPassivePack_EnduGainThree : AdvancedPassiveBase
{
    public override void OnRoundStartFirst()
    {
        base.owner?.bufListDetail?.AddKeywordBufThisRoundByEtc(KeywordBuf.WarpCharge, 5, base.owner);
    }

    public override void OnRoundEnd()
    {
        var charge = base.owner?.bufListDetail?.GetActivatedBuf(KeywordBuf.WarpCharge) as BattleUnitBuf_warpCharge;

        for (var i = 0; 3 > i; i++)
        {
            if (3 > charge?.stack)
            {
                break;
            }

            charge?.UseStack(3, false);

            if (charge is not null)
            {
                base.owner?.bufListDetail?.AddKeywordBufByEtc(KeywordBuf.Endurance, 1, base.owner);
                base.owner?.bufListDetail?.AddKeywordBufByEtc(KeywordBuf.Protection, 1, base.owner);
            }
        }
    }
}
