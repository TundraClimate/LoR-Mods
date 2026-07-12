public class PassiveAbility_TundraPassivePack_EnduToPow : AdvancedPassiveBase
{
    public override void OnRoundStartLast()
    {
        var endu = base.owner?.bufListDetail?.GetActivatedBuf(KeywordBuf.Endurance);

        if (endu is not null)
        {
            var num = endu.stack / 3;

            base.owner?.bufListDetail?.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, num, base.owner);
        }
    }
}
