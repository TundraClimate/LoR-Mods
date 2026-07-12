public class PassiveAbility_TundraPassivePack_Ignite : AdvancedPassiveBase
{
    public override bool TeamKill()
    {
        return true;
    }

    public override void OnSucceedAttack(BattleDiceBehavior behavior)
    {
        if (_fired.Contains(behavior.card.target) || behavior.card.target.faction != base.owner.faction)
        {
            return;
        }

        behavior.card.target.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Burn, 15, base.owner);

        _fired.Add(behavior.card.target);
    }

    private List<BattleUnitModel> _fired = new();
}
