namespace WarpClassFive_Card
{
    public class DiceCardSelfAbility_UseCharge5 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            (base.owner.bufListDetail.GetActivatedBuf(KeywordBuf.WarpCharge) as BattleUnitBuf_warpCharge)
                .UseStack(5, true);
        }

        public static string Desc = "[使用時] 充電5を消耗";

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
