public class BattleUnitBuf_TheFiveBuf : PrescriptBuf
{
    protected override string keywordId
    {
        get
        {
            return "TheFiveBuf";
        }
    }

    public override int OnGiveKeywordBufByCard(BattleUnitBuf cardBuf, int stack, BattleUnitModel target)
    {
        if (cardBuf.positiveType == BufPositiveType.Negative)
        {
            if (target != null && target.bufListDetail.HasBuf<BattleUnitBuf_TargetOfPrescript>())
            {
                this._debufs += stack;

                if (this._debufs >= 5)
                {
                    this.IsPassed = true;

                    if (base._owner.currentDiceAction.card.HasBuf<PassiveAbility_Prescript.BattleDiceCardBuf_IndexMark>())
                    {
                        this.IsPassedByTarget = true;
                    }
                }
            }
        }

        return base.OnGiveKeywordBufByCard(cardBuf, stack, target);
    }

    public override bool IsIndexMarkNeeds(BattleDiceCardModel model)
    {
        if (base._owner != null && base._owner.passiveDetail.HasPassive<PassiveAbility_Esther>())
        {
            LorId[] pids = new[] { new LorId(PowerfulEstherMOD.packageId, 26) };

            return pids.Contains(model.GetID());
        }

        return base.IsIndexMarkNeeds(model);
    }

    private int _debufs = 0;
}
