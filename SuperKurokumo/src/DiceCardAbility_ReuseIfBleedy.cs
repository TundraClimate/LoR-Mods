public class DiceCardAbility_ReuseIfBleedy : AdvancedDiceBase
{
    public static string Desc = "[的中] 相手に出血があるならこのダイスを再利用(最大1回)";

    public override void OnSucceedAttack(BattleUnitModel target)
    {
        if (!_reuse)
        {
            if (target?.GetBufStack<BattleUnitBuf_bleeding>() > 0)
            {
                _reuse = true;
                base.behavior?.isBonusAttack = true;
            }
        }
    }

    private bool _reuse;
}
