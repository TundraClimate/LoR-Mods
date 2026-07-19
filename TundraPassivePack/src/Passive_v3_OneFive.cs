using LOR_DiceSystem;

public class PassiveAbility_TundraPassivePack_OneFive : AdvancedPassiveBase
{
    public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
    {
        if (curCard.GetOriginalDiceBehaviorList().FindAll(beh => beh.Type != BehaviourType.Standby).Count == 1)
        {
            owner.battleCardResultLog?.SetPassiveAbility(this);

            curCard.ApplyDiceStatBonus(DiceMatch.AllDice, new AdvancedDiceStatBonus
            {
                ferocity = 50,
            });
        }
    }
}
