public class PassiveAbility_TundraPassivePack_BHealSmoke : AdvancedPassiveBase
{
    public override void OnChangeBufStack(BattleUnitBuf changed, int last)
    {
        if (changed.bufType is not KeywordBuf.Smoke)
        {
            return;
        }

        if (changed.stack > last)
        {
            base.owner?.breakDetail?.RecoverBreak((changed.stack - last.Max(0)) * 3);
        }
    }
}
