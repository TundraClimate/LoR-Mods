public class DiceCardSelfAbility_DeathSandBag : DiceCardSelfAbilityBase
{
    public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
    {
        foreach (BattleUnitModel model in BattleObjectManager.instance.GetAliveList(false).FindAll((BattleUnitModel mod) => mod.faction == Faction.Enemy))
        {
            model.Die();
        }
    }
}
