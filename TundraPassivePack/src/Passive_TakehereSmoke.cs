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
            var needs = (10 - lastStack);

            base.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Smoke, needs, base.owner);
            base.owner.TakeDamage(needs * 5, DamageType.Passive, base.owner);
        }
    }
}
