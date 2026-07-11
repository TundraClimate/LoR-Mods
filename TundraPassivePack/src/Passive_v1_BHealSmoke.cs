public class PassiveAbility_TundraPassivePack_BHealSmoke : AdvancedPassiveBase
{
    public override void OnAddBuf(BattleUnitBuf buf, int addedStack)
    {
        if (buf is BattleUnitBuf_smoke)
        {
            base.owner?.breakDetail?.RecoverBreak(addedStack * 3);
        }
    }
}
