public class DiceCardAbility_TheKillOrDamageD1 : DiceCardAbilityBase
{
    public static string Desc = "[マッチ勝利時] 相手の全てのダイスを破壊。";

    public override void OnWinParrying()
    {
        BattlePlayingCardDataInUnitModel targetCurrentPage = base.behavior.card.target.currentDiceAction;

        if (targetCurrentPage != null)
        {
            targetCurrentPage.DestroyDice(_ => true, DiceUITiming.AttackAfter);
        }
    }
}
