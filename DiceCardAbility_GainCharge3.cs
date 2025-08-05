
namespace WarpClassFive_Card
{
    public class DiceCardAbility_GainCharge3 : DiceCardAbilityBase
    {
        public override void OnWinParrying()
        {
            base.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.WarpCharge, 3, base.owner);
        }

        public static string Desc = "[マッチ勝利] 充電3を得る";

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
