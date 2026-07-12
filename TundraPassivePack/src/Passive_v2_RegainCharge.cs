public class PassiveAbility_TundraPassivePack_RegainCharge : AdvancedPassiveBase
{
    public override void OnChangeBufStack(BattleUnitBuf changed, int last)
    {
        if (last > changed.stack && changed is BattleUnitBuf_warpCharge)
        {
            _charge += last - changed.stack;

            if (_charge > 99)
            {
                _charge = 0;

                base.owner?.bufListDetail?.AddKeywordBufThisRoundByEtc(KeywordBuf.WarpCharge, 50, base.owner);
            }
        }
    }

    private int _charge;
}
