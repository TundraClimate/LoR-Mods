public class BattleUnitBuf_TheKillOrDamage : PrescriptBuf
{
    protected override string keywordId
    {
        get
        {
            return "TheKillOrDamage";
        }
    }

    public override void OnUseCard(BattlePlayingCardDataInUnitModel card)
    {
        this._tempHealth = card.target.hp;
    }

    public override void OnEndBattle(BattlePlayingCardDataInUnitModel curCard)
    {
        if (this._tempHealth != null && curCard != null)
        {
            if (curCard.card.HasBuf<PassiveAbility_Prescript.BattleDiceCardBuf_IndexMark>())
            {
                this._totalDamage += (int)(this._tempHealth - curCard.target.hp);

                if (this._totalDamage >= 29)
                {
                    this.IsPassed = true;

                    if (curCard.target.bufListDetail.HasBuf<BattleUnitBuf_TargetOfPrescript>())
                    {
                        this.IsPassedByTarget = true;
                    }
                }
            }
        }

        this._tempHealth = null;
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

    public override bool IsIndexMarkNeeds(BattleDiceCardModel model)
    {
        if (base._owner != null && base._owner.passiveDetail.HasPassive<PassiveAbility_Esther>())
        {
            LorId[] pids = new[] { new LorId(PowerfulEstherMOD.packageId, 21) };

            return pids.Contains(model.GetID());
        }

        return base.SetIndexMarkForAtkDice(model);
    }

    public override bool IsSelectable(PassiveAbilityBase self)
    {
        return base.SelectAtkDiceNeeds(self);
    }

    private int _totalDamage = 0;

    private float? _tempHealth;
}
