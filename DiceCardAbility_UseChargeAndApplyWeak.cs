
namespace WarpClassFive_Card
{
    public class DiceCardAbility_UseChargeAndApplyWeak : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            if (target != null)
            {
                BattleUnitBuf_warpCharge buff = base.owner.bufListDetail.GetActivatedBuf(KeywordBuf.WarpCharge) as BattleUnitBuf_warpCharge;
                if (buff.stack >= 9)
                {
                    target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Weak, 2, base.owner);
                    buff.UseStack(9, true);
                }
            }
        }

        public static string Desc = "[的中] 充電9を消費し、次の幕に虚弱2を付与する";

        public override string[] Keywords
        {
            get
            {
                return new string[]
                {
                    "Charge_Keyword",
                    "Weak_Keyword",
                };
            }
        }
    }
}
