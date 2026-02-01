public class DiceCardAbility_TheWinByHighD1 : DiceCardAbilityBase
{
    public static string Desc = "[マッチ開始時] このページの的中時効果が発動しない。";

    public override void OnRollDice()
    {
        if (base.owner != null && base.behavior != null && base.behavior.TargetDice != null)
        {
            foreach (BattleDiceBehavior beh in base.behavior.card.cardBehaviorQueue)
            {
                beh.abilityList.Clear();
            }
        }
    }
}
