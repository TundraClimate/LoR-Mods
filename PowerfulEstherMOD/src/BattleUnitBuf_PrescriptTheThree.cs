using LOR_DiceSystem;

public class BattleUnitBuf_TheThree : PrescriptBuf
{
    protected override string keywordId
    {
        get
        {
            return "TheThree";
        }
    }

    public override void OnSuccessAttack(BattleDiceBehavior behavior)
    {
        if (behavior.card.card.HasBuf<PassiveAbility_Prescript.BattleDiceCardBuf_IndexMark>())
        {
            if (behavior.card.target.bufListDetail.HasBuf<BattleUnitBuf_TargetOfPrescript>())
            {
                this._totalIndexHitByTarget++;
            }
            else
            {
                this._totalIndexHitByOther++;
            }
        }
        else
        {
            if (behavior.card.target.bufListDetail.HasBuf<BattleUnitBuf_TargetOfPrescript>())
            {
                this._totalHitByTarget++;
            }
            else
            {
                this._totalHitByOther++;
            }
        }

        int totalHit = this._totalIndexHitByTarget + this._totalIndexHitByOther + this._totalHitByTarget + this._totalHitByOther;

        if (totalHit >= 7)
        {
            this.IsPassed = true;
        }

        if (this._totalIndexHitByTarget + this._totalIndexHitByOther >= 3)
        {
            this.IsPassed = true;
        }

        if (this._totalIndexHitByTarget + this._totalHitByTarget >= 7)
        {
            this.IsPassedByTarget = true;
        }

        if (this._totalIndexHitByTarget >= 3)
        {
            this.IsPassedByTarget = true;
        }
    }

    public override bool IsIndexMarkNeeds(BattleDiceCardModel model)
    {
        if (base._owner != null && base._owner.passiveDetail.HasPassive<PassiveAbility_Esther>())
        {
            LorId[] pids = new[] { new LorId(PowerfulEstherMOD.packageId, 13) };

            return pids.Contains(model.GetID());
        }

        int count = 0;

        foreach (DiceBehaviour beh in ItemXmlDataList.instance.GetCardItem(model.GetID()).DiceBehaviourList)
        {
            if (beh.Type == BehaviourType.Atk)
            {
                count++;
            }
        }

        return count >= 3;
    }

    public override bool IsSelectable(PassiveAbilityBase self)
    {
        return base.SelectAtkDiceNeeds(self);
    }

    private int _totalHitByTarget = 0;

    private int _totalHitByOther = 0;

    private int _totalIndexHitByTarget = 0;

    private int _totalIndexHitByOther = 0;
}
