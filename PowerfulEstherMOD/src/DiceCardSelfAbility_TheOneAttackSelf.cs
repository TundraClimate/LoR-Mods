public class DiceCardSelfAbility_TheOneAttackSelf : DiceCardSelfAbilityBase
{
    public static string Desc = "[マッチ開始時] このページのダイス威力が7減少";

    public override void OnStartParrying()
    {
        BattlePlayingCardDataInUnitModel currentPage = base.owner.currentDiceAction;

        if (base.owner == null || currentPage == null)
        {
            return;
        }

        currentPage.ApplyDiceStatBonus(_ => true, new DiceStatBonus()
        {
            power = -7,
        });
    }
}
