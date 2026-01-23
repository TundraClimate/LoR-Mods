public class BattleUnitBuf_TheBreakOrKill : PrescriptBuf
{
    protected override string keywordId
    {
        get
        {
            return "TheBreakOrKill";
        }
    }

    public override void OnSuccessAttack(BattleDiceBehavior behavior)
    {
        BattleUnitModel target = behavior.card.target;

        if (target != null)
        {
            if (!target.breakDetail.IsBreakLifeZero())
            {
                return;
            }

            if (target.bufListDetail.HasBuf<BattleUnitBuf_TargetOfPrescript>())
            {
                this.IsPassedByTarget = true;
            }
            else
            {
                this.IsPassed = true;
            }
        }
    }

    public override void OnKill(BattleUnitModel target)
    {
        if (target != null)
        {
            if (target.bufListDetail.HasBuf<BattleUnitBuf_TargetOfPrescript>())
            {
                this.IsPassedByTarget = true;
            }
            else
            {
                this.IsPassed = true;
            }
        }
    }
}
