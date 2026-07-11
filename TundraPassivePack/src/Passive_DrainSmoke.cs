public class PassiveAbility_TundraPassivePack_DrainSmoke : AdvancedPassiveBase
{
    public override void OnRoundStart()
    {
        _remainder.Reset();
    }

    public override void OnSucceedAttack(BattleDiceBehavior behavior)
    {
        if (!_remainder.Remains)
        {
            return;
        }

        if (behavior?.card?.target?.TryGetBuf<BattleUnitBuf_smoke>(out var targetSmoke) is null or false)
        {
            return;
        }

        if (targetSmoke.stack == 0)
        {
            return;
        }

        targetSmoke.stack -= 1;

        var selfSmoke = base.owner.GetBufAndInitIfNull(() => new BattleUnitBuf_smoke());

        if (selfSmoke.stack == 10)
        {
            base.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.DmgUp, 1, base.owner);
        }
        else
        {
            base.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Smoke, 1, base.owner);
        }

        _remainder.Lose();
    }

    private Remainder _remainder = new(3);
}
