using LOR_DiceSystem;

public class BattleUnitBuf_TheRollAllRange : PrescriptBuf
{
    protected override string keywordId
    {
        get
        {
            return "TheRollAllRange";
        }
    }

    public override void OnRollDice(BattleDiceBehavior behavior)
    {
        int resValue = behavior.DiceVanillaValue;
        int resMin = behavior.GetDiceMin();
        int resMax = behavior.GetDiceMax();

        if (resValue == resMin)
        {
            this._hasMin = true;
        }

        if (resValue == resMax)
        {
            this._hasMax = true;
        }

        if (behavior.card.target != null && behavior.card.target.bufListDetail.HasBuf<BattleUnitBuf_TargetOfPrescript>())
        {
            if (this._hasMax || this._hasMin)
            {
                this.IsPassed = true;

                if (behavior.card.card.HasBuf<PassiveAbility_Prescript.BattleDiceCardBuf_IndexMark>())
                {
                    this.IsPassedByTarget = true;
                }
            }
        }

        if (this._hasMax && this._hasMin)
        {
            this.IsPassed = true;

            if (behavior.card.card.HasBuf<PassiveAbility_Prescript.BattleDiceCardBuf_IndexMark>())
            {
                this.IsPassedByTarget = true;
            }
        }
    }

    public override bool IsIndexMarkNeeds(BattleDiceCardModel model)
    {
        return ItemXmlDataList.instance.GetCardItem(model.GetID()).DiceBehaviourList.FindAll(dbh => dbh.Type != BehaviourType.Standby).Count != 1 && base.SetIndexMarkForAtkDice(model);
    }

    public override bool IsSelectable(PassiveAbilityBase self)
    {
        bool foundBiggerOneDice = self.Owner.allyCardDetail.GetHand().Exists(hand => ItemXmlDataList.instance.GetCardItem(hand.GetID()).DiceBehaviourList.FindAll(dbh => dbh.Type != BehaviourType.Standby).Count >= 2);

        return !foundBiggerOneDice && base.SelectAtkDiceNeeds(self);
    }

    private bool _hasMin = false;

    private bool _hasMax = false;
}
