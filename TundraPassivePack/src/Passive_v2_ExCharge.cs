public class PassiveAbility_TundraPassivePack_ExCharge : AdvancedPassiveBase
{
    public override int DrawCardAddr => (_consumed / 5).Min(3);

    public override void OnStartBattle()
    {
        _consumed = 0;
    }

    public override void OnRoundEndTheLast()
    {
        var buf = base.owner?.bufListDetail?.GetActivatedBuf(KeywordBuf.WarpCharge);

        if (buf is BattleUnitBuf_warpCharge charge)
        {
            var stack = charge.stack;

            charge.UseStack(stack, false);

            _consumed = stack;

            base.owner?.cardSlotDetail.RecoverPlayPoint((_consumed / 3).Min(6));
        }
    }

    private int _consumed;
}
