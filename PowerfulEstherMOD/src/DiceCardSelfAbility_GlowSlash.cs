public class DiceCardSelfAbility_GlowSlash : DiceCardSelfAbilityBase
{
    public static string Desc = "[マッチ開始時] このページのダイスの最大値が2減少";

    public override void OnStartParrying()
    {
        BattlePlayingCardDataInUnitModel currentPage = base.owner.currentDiceAction;

        if (base.owner == null || currentPage == null)
        {
            return;
        }

        if (currentPage.target.currentDiceAction == null)
        {
            return;
        }

        currentPage.ApplyDiceStatBonus(_ => true, new DiceStatBonus()
        {
            max = -2,
        });
    }
}
