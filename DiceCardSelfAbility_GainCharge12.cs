namespace WarpClassFive_Card
{
    public class DiceCardSelfAbility_GainCharge12 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.WarpCharge, 12, base.owner);
        }
    }
}
