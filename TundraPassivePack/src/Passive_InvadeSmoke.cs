public class PassiveAbility_TundraPassivePack_InvadeSmoke : AdvancedPassiveBase
{
    public override void BeforeRollDice(BattleDiceBehavior behavior)
    {
        var stack = base.owner?.GetBufStack<BattleUnitBuf_smoke>() ?? 0;

        if (stack > 0)
        {
            behavior.ApplyDiceStatBonus(new DiceStatBonus() { dmgRate = stack * 2 });
        }
    }
}
