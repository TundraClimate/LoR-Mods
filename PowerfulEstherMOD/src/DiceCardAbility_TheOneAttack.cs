using LOR_DiceSystem;

public class DiceCardAbility_TheOneAttack : DiceCardAbilityBase
{
    public static string Desc = "防御ダイスとマッチしたなら、威力が8増加";

    public override void BeforeRollDice_Target(BattleDiceBehavior targetDice)
    {
        if (targetDice != null)
        {
            if (targetDice.Detail == BehaviourDetail.Guard)
            {
                if (base.behavior != null)
                {
                    base.behavior.ApplyDiceStatBonus(new DiceStatBonus()
                    {
                        power = 8,
                    });
                }
            }
        }
    }
}
