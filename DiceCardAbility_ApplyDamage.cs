
namespace WarpClassFive_Card
{
    public class DiceCardAbility_ApplyDamage : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            if (target != null)
            {
                target.TakeDamage(18, DamageType.Attack, base.owner);
            }
        }
    }
}
