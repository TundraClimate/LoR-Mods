using System.Collections.Generic;

public class BattleUnitBuf_TheBreakOrKill : PrescriptBuf
{
    protected override string keywordId
    {
        get
        {
            return "TheBreakOrKill";
        }
    }

    public override void OnRollDice(BattleDiceBehavior behavior)
    {
        this._lastDice = behavior;
    }

    public override void OnEndBattle(BattlePlayingCardDataInUnitModel curCard)
    {
        if (curCard == null)
        {
            return;
        }

        BattleUnitModel target = curCard.target;

        if (target != null)
        {
            if (!target.breakDetail.IsBreakLifeZero())
            {
                return;
            }

            if (curCard.card.HasBuf<PassiveAbility_Prescript.BattleDiceCardBuf_IndexMark>() || this._lastDice.abilityList.Exists(abi => abi is PassiveAbility_Prescript.DiceCardAbility_Marker))
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

    public override void OnKill(BattleUnitModel target)
    {
        if (target != null)
        {
            if (base._owner.currentDiceAction.card.HasBuf<PassiveAbility_Prescript.BattleDiceCardBuf_IndexMark>() || this._lastDice.abilityList.Exists(abi => abi is PassiveAbility_Prescript.DiceCardAbility_Marker))
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

    public override bool IsIndexMarkNeeds(BattleDiceCardModel model)
    {
        if (base._owner != null && base._owner.passiveDetail.HasPassive<PassiveAbility_Esther>())
        {
            LorId[] pids = new[] { new LorId(PowerfulEstherMOD.packageId, 22) };

            return pids.Contains(model.GetID());
        }

        return base.IsIndexMarkNeeds(model);
    }

    public override BattleUnitModel FixedIndexTarget(List<BattleUnitModel> candidates, BattleUnitModel origin)
    {
        BattleUnitModel res = origin;

        foreach (BattleUnitModel candidate in candidates)
        {
            if (res.breakDetail.breakGauge > candidate.breakDetail.breakGauge && !candidate.breakDetail.IsBreakLifeZero())
            {
                res = candidate;
            }
        }

        return res;
    }

    private BattleDiceBehavior _lastDice;
}
