public class DiceCardSelfAbility_TheNormalResistSelf : DiceCardSelfAbilityBase
{
    public static string Desc = "[マッチ開始時] このページでマッチ敗北時、このページの威力が2減少。";

    public override void OnLoseParrying()
    {
        this._isLosed = true;
    }

    public override void BeforeRollDice(BattleDiceBehavior behavior)
    {
        if (base.owner != null && behavior != null)
        {
            if (this._isLosed)
            {
                behavior.ApplyDiceStatBonus(new DiceStatBonus()
                {
                    power = -2,
                });
            }
        }
    }

    private bool _isLosed = false;
}
