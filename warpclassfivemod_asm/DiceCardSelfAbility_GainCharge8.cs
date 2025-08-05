namespace WarpClassFive_Card
{
    public class DiceCardSelfAbility_GainCharge8 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            base.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.WarpCharge, 8, base.owner);
        }

        public static string Desc = "[使用時] 充電8を得る";

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
