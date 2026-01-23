using LOR_DiceSystem;

public class BattleUnitBuf_TheWinAndLose : PrescriptBuf
{
    protected override string keywordId
    {
        get
        {
            return "TheWinAndLose";
        }
    }

    public override void OnWinParrying(BattleDiceBehavior behavior)
    {
        if (behavior.card.slotOrder != 0)
        {
            return;
        }

        this._isWinned = true;

        this.AfterParry(behavior);
    }

    public override void OnLoseParrying(BattleDiceBehavior behavior)
    {
        if (behavior.card.slotOrder != 0)
        {
            return;
        }

        this._isLosed = true;

        this.AfterParry(behavior);
    }

    private void AfterParry(BattleDiceBehavior behavior)
    {
        if (this._isLosed && this._isWinned)
        {
            this.IsPassed = true;

            if (behavior.card.card.HasBuf<PassiveAbility_Prescript.BattleDiceCardBuf_IndexMark>() || behavior.card.target.bufListDetail.HasBuf<BattleUnitBuf_TargetOfPrescript>())
            {
                this.IsPassedByTarget = true;
            }
        }
    }

    public override bool IsIndexMarkNeeds(BattleDiceCardModel model)
    {
        return ItemXmlDataList.instance.GetCardItem(model.GetID()).DiceBehaviourList.FindAll(dbh => dbh.Type != BehaviourType.Standby).Count != 1;
    }

    public override bool IsSelectable(PassiveAbilityBase self)
    {
        bool foundOneDicePage = self.Owner.allyCardDetail.GetHand().Find(hand => ItemXmlDataList.instance.GetCardItem(hand.GetID()).DiceBehaviourList.FindAll(dbh => dbh.Type != BehaviourType.Standby).Count == 1) != null;

        return !foundOneDicePage;
    }

    private bool _isLosed = false;

    private bool _isWinned = false;
}
