public class PassiveAbility_TundraPassivePack_SupplySmoke : AdvancedPassiveBase
{
    public override void OnRoundEnd_before()
    {
        base.owner?.bufListDetail?.AddKeywordBufThisRoundByEtc(KeywordBuf.Smoke, 1, base.owner);
    }
}
