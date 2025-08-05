namespace WarpClassFive_Card
{
    public class DiceCardSelfAbility_GainChargeAndLight : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.WarpCharge, 6, base.owner);
            base.owner.cardSlotDetail.RecoverPlayPointByCard(1);
        }

        public static string Desc = "[使用時] 充電6を得る。光を1回復";

        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                    "Charge_Keyword",
                    "Energy_keyword"
                };
            }
        }
    }
}
