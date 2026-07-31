public class PassiveAbility_TundraPassivePack_SecondAttack : AdvancedPassiveBase
{
    public override void BeforeRollDice(BattleDiceBehavior behavior)
    {
        behavior.ApplyDiceStatBonus(new AdvancedDiceStatBonus { ferocity = 100 });
    }
}
