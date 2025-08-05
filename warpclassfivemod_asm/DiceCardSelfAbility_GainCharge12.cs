namespace WarpClassFive_Card
{
    public class DiceCardSelfAbility_GainCharge12 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.WarpCharge, 12, base.owner);
        }

        public static string Desc = "[使用時] 充電12を得る";

        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                    "Charge_Keyword"
                };
            }
        }
    }
}
