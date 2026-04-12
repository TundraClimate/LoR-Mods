public class PassiveAbility_TundraPassivePack_BHealSmoke : AdvancedPassiveBase
{
    public override void OnAddKeywordBufByCardForEvent(KeywordBuf keywordBuf, int stack, BufReadyType readyType)
    {
        if (keywordBuf is KeywordBuf.Smoke)
        {
            base.owner?.breakDetail?.RecoverBreak(stack * 3);
        }
    }
}
