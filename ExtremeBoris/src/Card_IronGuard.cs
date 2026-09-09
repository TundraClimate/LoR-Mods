public class DiceCardSelfAbility_ExtremeBoris_IronGuard : AdvancedCardBase
{
    public override void OnUseCard()
    {
        base.owner.bufListDetail.AddKeywordBufThisRoundByCard(LimKeywordBuf.Poise, 8, base.owner);
    }
}
