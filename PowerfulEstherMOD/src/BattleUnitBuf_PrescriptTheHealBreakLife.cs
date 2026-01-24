using LOR_DiceSystem;

public class BattleUnitBuf_TheHealBreakLife : PrescriptBuf
{
    protected override string keywordId
    {
        get
        {
            return "TheHealBreakLife";
        }
    }

    public override void OnWinParrying(BattleDiceBehavior behavior)
    {
        if (!behavior.card.card.HasBuf<PassiveAbility_Prescript.BattleDiceCardBuf_IndexMark>())
        {
            return;
        }

        BehaviourDetail selfDet = behavior.Detail;
        BehaviourDetail otherDet = behavior.TargetDice.Detail;

        if (selfDet != BehaviourDetail.Evasion && otherDet == BehaviourDetail.Evasion)
        {
            return;
        }

        int selfPow = behavior.DiceResultValue;
        int otherPow = behavior.TargetDice.DiceResultValue;

        this._totalHeals += selfPow - otherPow;

        if (this._totalHeals >= 18)
        {
            this.IsPassed = true;

            if (behavior.TargetDice.owner.bufListDetail.HasBuf<BattleUnitBuf_TargetOfPrescript>())
            {
                this.IsPassedByTarget = true;
            }
        }
    }

    public override void OnEndBattle(BattlePlayingCardDataInUnitModel curCard)
    {
        this._totalHeals = 0;
    }

    public override bool IsIndexMarkNeeds(BattleDiceCardModel model)
    {
        return ItemXmlDataList.instance.GetCardItem(model.GetID()).DiceBehaviourList.Exists(beh => beh.Detail == BehaviourDetail.Evasion && beh.Type != BehaviourType.Standby);
    }

    public override bool IsSelectable(PassiveAbilityBase self)
    {
        return self.Owner.allyCardDetail.GetHand().Exists(hand => ItemXmlDataList.instance.GetCardItem(hand.GetID()).DiceBehaviourList.Exists(beh => beh.Detail == BehaviourDetail.Evasion && beh.Type != BehaviourType.Standby));
    }

    private int _totalHeals = 0;
}
