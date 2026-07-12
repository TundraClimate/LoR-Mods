public class PassiveAbility_TundraPassivePack_StealCharge : AdvancedPassiveBase
{
    public override void AfterGiveDamage(int damage)
    {
        if (damage >= 15)
        {
            base.owner?.bufListDetail?.AddKeywordBufThisRoundByEtc(KeywordBuf.WarpCharge, 1, base.owner);
        }
    }
}
