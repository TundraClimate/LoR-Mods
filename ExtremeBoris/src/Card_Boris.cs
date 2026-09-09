public class DiceCardSelfAbility_ExtremeBoris_Boris : AdvancedCardBase
{
    public override void OnUseCard()
    {
        card.ignorePower = true;

        base.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.DmgUp, 2, base.owner);
    }

    public override void OnStartParrying()
    {
        if (card?.target?.currentDiceAction is not null)
        {
            card.target.currentDiceAction.ignorePower = true;
        }
    }
}
