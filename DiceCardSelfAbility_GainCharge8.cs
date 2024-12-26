namespace WarpClassFive_Card
{
    public class DiceCardSelfAbility_GainCharge8 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.WarpCharge, 8, base.owner);
        }
    }
}
