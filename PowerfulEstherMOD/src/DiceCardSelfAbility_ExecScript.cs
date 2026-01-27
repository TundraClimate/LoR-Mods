public class DiceCardSelfAbility_ExecScript : DiceCardSelfAbilityBase
{
    public static string Desc = "[マッチ開始時] 指令の印が刻まれているなら、2つ目のダイスの威力が5増加";

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

        if (currentPage.card.HasBuf<PassiveAbility_Prescript.BattleDiceCardBuf_IndexMark>())
        {
            currentPage.ApplyDiceStatBonus(dice => dice.index == 1, new DiceStatBonus()
            {
                power = 5,
            });
        }
    }
}
