public class DiceCardAbility_TheRollAllRange : DiceCardAbilityBase
{
    public static string Desc = "[的中] 出目が最大値なら、このダイスの威力を2減少させてダイスを再利用(最大4回)。";

    public override void OnSucceedAttack()
    {
        if (base.behavior != null && this.behavior.DiceVanillaValue == this.behavior.GetDiceVanillaMax())
        {
            if (this._count > 4)
            {
                return;
            }

            if (4 >= this._count)
            {
                base.ActivateBonusAttackDice();
                base.behavior.ApplyDiceStatBonus(new DiceStatBonus()
                {
                    power = -2,
                });
                this._count++;
            }
        }
    }

    private int _count = 1;
}
