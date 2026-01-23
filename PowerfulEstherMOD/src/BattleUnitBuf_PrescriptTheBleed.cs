using LOR_DiceSystem;

public class BattleUnitBuf_TheBleed : PrescriptBuf
{
    protected override string keywordId
    {
        get
        {
            return "TheBleed";
        }
    }

    public override void BeforeRollDice(BattleDiceBehavior behavior)
    {
        if (behavior.Detail == BehaviourDetail.Guard || behavior.Detail == BehaviourDetail.Evasion)
        {
            return;
        }

        if (behavior.owner.bufListDetail.HasBuf<BattleUnitBuf_bleeding>() && behavior.card.target.bufListDetail.HasBuf<BattleUnitBuf_TargetOfPrescript>())
        {
            this._isBleedByTarget = true;
        }
    }

    public override bool IsImmuneDmg(DamageType type, KeywordBuf keyword = KeywordBuf.None)
    {
        if (type == DamageType.Buf && keyword == KeywordBuf.Bleeding)
        {
            this._isBleeding = true;
        }

        return base.IsImmuneDmg(type, keyword);
    }

    public override void OnLoseHp(int dmg)
    {
        if (this._isBleeding)
        {
            this._totalBleedDmg += dmg;

            if (this._totalBleedDmg >= 21)
            {
                this.IsPassed = true;

                if (this._isBleedByTarget)
                {
                    this.IsPassedByTarget = true;
                }
            }

            this._isBleeding = false;
        }
    }

    public override bool IsIndexMarkNeeds(BattleDiceCardModel model)
    {
        return base.SetIndexMarkForAtkDice(model);
    }

    public override bool IsSelectable(PassiveAbilityBase self)
    {
        return self.Owner.bufListDetail.HasBuf<BattleUnitBuf_bleeding>();
    }

    private bool _isBleeding = false;

    private int _totalBleedDmg = 0;

    private bool _isBleedByTarget = false;
}
