public class BattleUnitBuf_TheOverWinMatch : PrescriptBuf
{
    protected override string keywordId
    {
        get
        {
            return "TheOverWinMatch";
        }
    }

    public override void OnWinParrying(BattleDiceBehavior behavior)
    {
        if (behavior.DiceResultValue - behavior.TargetDice.DiceResultValue >= 5)
        {
            this.IsPassed = true;

            if (behavior.TargetDice.owner.bufListDetail.HasBuf<BattleUnitBuf_TargetOfPrescript>())
            {
                this.IsPassedByTarget = true;
            }
        }
    }
}
