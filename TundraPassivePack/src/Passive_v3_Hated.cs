public class PassiveAbility_TundraPassivePack_Hated : AdvancedPassiveBase
{
    public override void OnDie()
    {
        foreach (var unit in base.owner.faction.AliveUnits)
        {
            if (unit != base.owner)
            {
                unit.breakDetail.RecoverBreak(unit.breakDetail.GetDefaultBreakGauge());
            }
        }
    }
}
