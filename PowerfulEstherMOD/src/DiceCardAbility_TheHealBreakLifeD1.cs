using System.Collections.Generic;

public class DiceCardAbility_TheHealBreakLifeD1 : DiceCardAbilityBase
{
    public static string Desc = "<color=green>連結ダイス-a</color>";

    public override void OnLoseParrying()
    {
        if (base.owner == null)
        {
            return;
        }

        BattleKeepedCardDataInUnitModel keepCard = base.owner.cardSlotDetail.keepCard;

        List<BattleDiceBehavior> dices = keepCard.GetDiceBehaviorList();

        keepCard.RemoveAllDice();

        foreach (BattleDiceBehavior dice in dices)
        {
            if (!dice.abilityList.Exists(abi => abi is DiceCardAbility_TheHealBreakLifeD3))
            {
                keepCard.AddDice(dice);
            }
        }
    }
}
