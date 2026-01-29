public class DiceCardAbility_TheKillOrDamageD2 : DiceCardAbilityBase
{
    public static string Desc = "[的中] 相手に4ダメージ。このダイスを再利用。(最大4回)";

    public override void OnSucceedAttack(BattleUnitModel target)
    {
        if (target == null || base.behavior == null)
        {
            return;
        }

        target.TakeDamage(4, DamageType.Card_Ability, base.owner);

        if (this._count < 4)
        {
            base.ActivateBonusAttackDice();

            this._count++;
        }
    }

    private int _count = 0;
}
