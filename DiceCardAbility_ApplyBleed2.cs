namespace WarpClassFive_Card
{
    public class DiceCardAbility_ApplyBleed2 : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            if (target != null)
            {
                target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Bleeding, 2, base.owner);
            }
        }

        public static string Desc = "[的中] 次の幕に出血2を付与";

        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                    "Bleed_Keyword"
                };
            }
        }
    }
}
