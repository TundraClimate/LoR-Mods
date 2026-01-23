public class BattleUnitBuf_TheOneSpeedDice : PrescriptBuf
{
    protected override string keywordId
    {
        get
        {
            return "TheOneSpeedDice";
        }
    }

    public override void OnSuccessAttack(BattleDiceBehavior behavior)
    {
        if (behavior.card.slotOrder != 0)
        {
            return;
        }

        if (behavior.card.card.HasBuf<PassiveAbility_Prescript.BattleDiceCardBuf_IndexMark>())
        {
            this._totalHit++;

            if (this._totalHit >= 2)
            {
                this._isBonus = true;
            }
        }
        else
        {
            this._totalHit++;
        }

        if (behavior.card.target.bufListDetail.HasBuf<BattleUnitBuf_TargetOfPrescript>())
        {
            this._isBonus = true;
        }

        if (this._totalHit >= 2)
        {
            this.IsPassed = true;

            if (this._isBonus)
            {
                this.IsPassedByTarget = true;
            }
        }
    }

    public override bool IsIndexMarkNeeds(BattleDiceCardModel model)
    {
        return base.SetIndexMarkForAtkDice(model);
    }

    public override bool IsSelectable(PassiveAbilityBase self)
    {
        return base.SelectAtkDiceNeeds(self);
    }

    private int _totalHit = 0;

    private bool _isBonus = false;
}
