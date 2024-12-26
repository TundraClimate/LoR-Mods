namespace WarpClassFive_Card
{
    public class DiceCardSelfAbility_SwapChargeMode : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Protection, 3, base.owner);
            base.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Weak, 2, base.owner);
            base.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.WarpCharge, 10, base.owner);
            base.owner.cardSlotDetail.RecoverPlayPointByCard(4);
        }
    }
}
