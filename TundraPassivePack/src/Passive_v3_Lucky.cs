public class PassiveAbility_TundraPassivePack_Lucky : AdvancedPassiveBase
{
    public override void BeforeRollDice(BattleDiceBehavior behavior)
    {
        behavior.ApplyDiceStatBonus(new AdvancedDiceStatBonus { highrollGlobalWeight = 100 });
    }
}
