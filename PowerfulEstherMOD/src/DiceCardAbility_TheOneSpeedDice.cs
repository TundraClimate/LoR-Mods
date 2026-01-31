public class DiceCardAbility_TheOneSpeedDice : DiceCardAbilityBase
{
    public static string Desc = "このページを使用している速度ダイスの出目だけ威力が増加。";

    public override void BeforeRollDice()
    {
        if (this.behavior != null)
        {
            int speed = behavior.card.speedDiceResultValue;

            this.behavior.ApplyDiceStatBonus(new DiceStatBonus()
            {
                power = speed,
            });
        }
    }
}
