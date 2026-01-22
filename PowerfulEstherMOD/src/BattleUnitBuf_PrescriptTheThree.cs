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

        if (this._totalHitByTarget + this._totalHitByOther > 3)
        {
            this.IsPassed = true;
        }

        if (this._totalIndexHitByTarget + this._totalIndexHitByOther >= 3)
        {
            this.IsPassed = true;
        }

        if (this._totalHitByTarget > 3)
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
