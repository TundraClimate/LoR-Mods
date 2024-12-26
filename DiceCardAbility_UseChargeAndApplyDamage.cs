
namespace WarpClassFive_Card
{
    public class DiceCardAbility_UseChargeAndApplyDamage : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            if (target != null)
            {
                BattleUnitBuf_warpCharge buff = base.owner.bufListDetail.GetActivatedBuf(KeywordBuf.WarpCharge) as BattleUnitBuf_warpCharge;
                if (buff.stack >= 9)
                {
                    target.TakeDamage(12, DamageType.Attack, base.owner);
                    buff.UseStack(9, true);
                }
            }
        }
    }
}
