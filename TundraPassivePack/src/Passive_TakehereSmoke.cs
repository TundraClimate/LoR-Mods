public class PassiveAbility_TundraPassivePack_TakehereSmoke : AdvancedPassiveBase
{
    public override void OnRoundStartFirst()
    {
        if (base.owner is null)
        {
            return;
        }

        var lastStack = base.owner.GetBufStack<BattleUnitBuf_smoke>() ?? 0;

        if (10 > lastStack)
        {
            var smoke = base.owner.GetBufAndInitIfNull(() => new BattleUnitBuf_smoke());

            smoke.stack = 10;

            base.owner.TakeDamage((10 - lastStack) * 5, DamageType.Passive, base.owner);
        }
    }
}
