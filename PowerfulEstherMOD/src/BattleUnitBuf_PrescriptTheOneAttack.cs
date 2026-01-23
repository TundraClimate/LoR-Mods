public class BattleUnitBuf_TheOneAttack : PrescriptBuf
{
    protected override string keywordId
    {
        get
        {
            return "TheOneAttack";
        }
    }

    public override void OnSuccessAttack(BattleDiceBehavior behavior)
    {
        if (behavior.card.card.HasBuf<PassiveAbility_Prescript.BattleDiceCardBuf_IndexMark>())
        {
            this.IsPassed = !this._using;

            if (this._using)
            {
                this._isFailed = true;
            }

            if (this._isFailed)
            {
                this.IsPassed = false;
            }

            this._using = true;

            if (behavior.card.target.bufListDetail.HasBuf<BattleUnitBuf_TargetOfPrescript>())
            {
                this.IsPassedByTarget = this.IsPassed;
            }
        }
    }

    public override void OnEndBattle(BattlePlayingCardDataInUnitModel curCard)
    {
        this._using = false;
    }

    private bool _isFailed = false;

    private bool _using = false;

    public override bool IsIndexMarkNeeds(BattleDiceCardModel model)
    {
        if (base._owner != null && base._owner.passiveDetail.HasPassive<PassiveAbility_Esther>())
        {
            LorId[] pids = new[] { new LorId(PowerfulEstherMOD.packageId, 11) };

            return pids.Contains(model.GetID());
        }

        return base.SetIndexMarkForAtkDice(model);
    }

    public override bool IsSelectable(PassiveAbilityBase self)
    {
        return base.SelectAtkDiceNeeds(self);
    }
}
