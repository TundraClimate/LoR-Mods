public class DiceCardAbility_TheBreakOrKillD1 : DiceCardAbilityBase
{
    public static string Desc = "[的中] このページの防御ダイス威力が2増加。";

    public override void OnSucceedAttack()
    {
        if (base.owner == null || base.owner.currentDiceAction == null)
        {
            return;
        }

        base.owner.currentDiceAction.ApplyDiceStatBonus(DiceMatch.AllDefenseDice, new DiceStatBonus()
        {
            power = 2,
        });
    }
}
