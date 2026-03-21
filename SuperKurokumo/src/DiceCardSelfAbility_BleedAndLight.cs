public class DiceCardSelfAbility_BleedAndLight : AdvancedCardBase
{
    public static string Desc = "[使用時] 相手が出血を保有しているなら、光を3回復";

    public override void OnUseCard()
    {
        if (base.owner?.currentDiceAction?.target?.GetBufStack<BattleUnitBuf_bleeding>() > 0)
        {
            base.owner?.cardSlotDetail?.RecoverPlayPoint(3);
        }
    }
}
