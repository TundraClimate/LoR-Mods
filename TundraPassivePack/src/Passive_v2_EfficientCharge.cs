public class PassiveAbility_TundraPassivePack_EfficientCharge : AdvancedPassiveBase
{
    public override void OnGainChargeStack()
    {
        base.owner?.bufListDetail?.AddKeywordBufThisRoundByEtc(KeywordBuf.WarpCharge, 2, base.owner);
    }

    public override void OnUseChargeStack()
    {
        (base.owner?.bufListDetail?.GetActivatedBuf(KeywordBuf.WarpCharge) as BattleUnitBuf_warpCharge)?.UseStack(3, false);
    }
}
