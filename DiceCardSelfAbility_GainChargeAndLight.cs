namespace WarpClassFive_Card
{
    public class DiceCardSelfAbility_GainChargeAndLight : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.WarpCharge, 6, base.owner);
            base.owner.cardSlotDetail.RecoverPlayPointByCard(1);
        }
    }
}
