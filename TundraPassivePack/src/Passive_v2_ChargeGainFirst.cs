public class PassiveAbility_TundraPassivePack_ChargeGainFirst : AdvancedPassiveBase
{
    public override void OnWaveStart()
    {
        base.owner?.bufListDetail?.AddKeywordBufThisRoundByEtc(KeywordBuf.WarpCharge, 9999, base.owner);

        var stack = base.owner?.bufListDetail?.GetActivatedBuf(KeywordBuf.WarpCharge)?.stack ?? 0;

        if (stack != 0)
        {
            base.owner?.TakeDamage(stack, DamageType.Passive, base.owner);
        }
    }
}
