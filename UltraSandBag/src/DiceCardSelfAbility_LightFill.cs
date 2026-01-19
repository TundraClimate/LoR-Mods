public class DiceCardSelfAbility_LightFill : DiceCardSelfAbilityBase
{
    public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
    {
        if (unit == null || unit.MaxPlayPoint == unit.PlayPoint)
        {
            return;
        }

        unit.cardSlotDetail.RecoverPlayPointByCard(unit.MaxPlayPoint - unit.PlayPoint);
    }
}
