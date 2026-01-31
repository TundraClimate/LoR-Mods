public class DiceCardSelfAbility_TheOneSpeedDiceSelf : DiceCardSelfAbilityBase
{
    public static string Desc = "[マッチ開始時] 相手のマッチ威力が2減少。";

    public override void OnStartParrying()
    {
        if (base.owner != null && base.owner.currentDiceAction.target.currentDiceAction != null)
        {
            base.owner.currentDiceAction.target.currentDiceAction.ApplyDiceStatBonus(_ => true, new DiceStatBonus()
            {
                power = -2,
            });
        }
    }
}
