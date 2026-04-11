public class PassiveAbility_TundraPassivePack_DrawSmoke : AdvancedPassiveBase
{
    public override void OnRoundStart()
    {
        _isDraw = false;
    }

    public override void OnSucceedAttack(BattleDiceBehavior behavior)
    {
        if (_isDraw)
        {
            return;
        }

        if (base.owner?.TryGetBuf<BattleUnitBuf_smoke>(out var smoke) == true && smoke.stack >= 3)
        {
            _isDraw = true;

            smoke.stack -= 3;

            base.owner?.allyCardDetail?.DrawCards(1);
        }
    }

    private bool _isDraw = false;
}
