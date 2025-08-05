namespace WarpClassFive_Card
{
    public class DiceCardSelfAbility_UseAllChargeAndGainPower : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            BattleUnitBuf_warpCharge activatedBuf = base.owner.bufListDetail.GetActivatedBuf(KeywordBuf.WarpCharge) as BattleUnitBuf_warpCharge;
            int stack = activatedBuf.stack;
            int buff = stack / 2;
            activatedBuf.UseStack(stack, true);
            base.card.ApplyDiceStatBonus(DiceMatch.AllAttackDice, new DiceStatBonus { power = buff });
        }

        public static string Desc = 
          "[使用時] 充電を全て消費し、消費した充電÷2(少数以下切り捨て)だけこのページの全ダイスの威力が増加";

        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                    "Charge_Keyword",
                };
            }
        }
    }
}
