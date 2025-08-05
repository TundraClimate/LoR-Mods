
namespace WarpClassFive_Card
{
    public class DiceCardAbility_UseChargeAndApplyFewDamage : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            if (target != null)
            {
                BattleUnitBuf_warpCharge buff = base.owner.bufListDetail.GetActivatedBuf(KeywordBuf.WarpCharge) as BattleUnitBuf_warpCharge;
                if (buff.stack >= 8)
                {
                    target.TakeDamage(10, DamageType.Attack, base.owner);
                    buff.UseStack(8, true);
                }
            }
        }

        public static string Desc = "[的中] 充電8を消耗し、相手に10ダメージ";

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
