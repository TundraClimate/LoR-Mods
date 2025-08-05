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

        public static string Desc = "[使用時] 次の幕に保護3、充電10、虚弱2を得る。光を4回復する";

        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                    "Charge_Keyword",
                    "Energy_Keyword",
                    "Weak_Keyword",
                    "Protection_Keyword",
                };
            }
        }
    }
}
