public class PassiveAbility_TundraPassivePack_BHealSmoke : AdvancedPassiveBase
{
    public override void OnChangeBufStack(BattleUnitBuf changed, int last)
    {
        if (changed.bufType is not KeywordBuf.Smoke)
        {
            return;
        }

        if (0 > last)
        {
            last = 0;
        }

        if (changed.stack > last)
        {
            base.owner?.breakDetail?.RecoverBreak((changed.stack - last) * 3);
        }
    }
}
