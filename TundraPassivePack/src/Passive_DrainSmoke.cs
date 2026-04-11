public class PassiveAbility_TundraPassivePack_DrainSmoke : AdvancedPassiveBase
{
    public override void OnRoundStart()
    {
        _count = 0;
    }

    public override void OnSucceedAttack(BattleDiceBehavior behavior)
    {
        if (_count >= 3)
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
            var dmgUp = base.owner.GetBufAndInitIfNull(() => new BattleUnitBuf_dmgUp());

            dmgUp.stack += 1;
        }
        else
        {
            selfSmoke.stack += 1;
        }

        _count += 1;
    }

    private int _count = 0;
}
