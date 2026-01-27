public class DiceCardSelfAbility_AcceptOrder : DiceCardSelfAbilityBase
{
    public static string Desc = "[使用時] 「唯一」状態なら、最低値と最大値が3ずつ増加。";

    public override void OnUseCard()
    {
        if (base.owner == null)
        {
            return;
        }

        if (base.owner.allyCardDetail.IsHighlander())
        {
            base.owner.currentDiceAction.ApplyDiceStatBonus(_ => true, new DiceStatBonus()
            {
                min = 3,
                max = 3,
            });
        }
    }
}
