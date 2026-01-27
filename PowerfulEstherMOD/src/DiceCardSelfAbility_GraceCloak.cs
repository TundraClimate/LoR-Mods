public class DiceCardSelfAbility_GraceCloak : DiceCardSelfAbilityBase
{
    public static string Desc = "[戦闘開始時] 指令の印が付いているなら、全てのダイスの最低値が3増加。";

    public override void OnStartBattleAfterCreateBehaviour()
    {
        BattlePlayingCardDataInUnitModel baseCard = base.card;

        if (base.owner == null || baseCard == null)
        {
            return;
        }

        if (baseCard.card.HasBuf<PassiveAbility_Prescript.BattleDiceCardBuf_IndexMark>())
        {
            foreach (BattleDiceBehavior beh in base.owner.cardSlotDetail.keepCard.GetDiceBehaviorList())
            {
                if (beh.behaviourInCard.Script == "GraceCloakDice")
                {
                    beh.ApplyDiceStatBonus(new DiceStatBonus()
                    {
                        min = 3,
                    });
                }
            }
        }

        return;
    }

    private class DiceCardAbility_GraceCloakDice : DiceCardAbilityBase
    {
    }
}
