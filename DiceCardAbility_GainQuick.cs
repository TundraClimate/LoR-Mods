
namespace WarpClassFive_Card
{
    public class DiceCardAbility_GainQuick : DiceCardAbilityBase
    {
        public override void OnSucceedAttack()
        {
            base.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 2, base.owner);
        }

        public static string Desc = "[的中] 次の幕にクイック2を得る";

        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                    "Quickness_Keyword"
                };
            }
        }
    }
}
