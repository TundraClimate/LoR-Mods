using LimbufOfHermes;

public class PassiveAbility_TundraPassivePack_Critical : AdvancedPassiveBase
{
    public override void OnRoundStart()
    {
        base.owner.bufListDetail.AddKeywordBufThisRoundByEtc(LimKeywordBuf.Poise, 20, base.owner);
    }

    public override void BeforeRollDice(BattleDiceBehavior behavior)
    {
        behavior.ApplyCritDamageAdder(1.3);
    }
}
