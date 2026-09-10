public class PassiveAbility_ExtremeBoris_Power : AdvancedPassiveBase
{
    public override void BeforeRollDice(BattleDiceBehavior behavior)
    {
        var dmgUp = base.owner.bufListDetail.GetKewordBufStack(KeywordBuf.DmgUp);

        if (!behavior.isBonusAttack && dmgUp > 0)
        {
            behavior.ApplyCritDamageAdder(dmgUp.Min(20) * 0.05);
        }
    }
}
