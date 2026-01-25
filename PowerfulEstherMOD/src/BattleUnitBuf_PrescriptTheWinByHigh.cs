using LOR_DiceSystem;

public class BattleUnitBuf_TheWinByHigh : PrescriptBuf
{
    protected override string keywordId
    {
        get
        {
            return "TheWinByHigh";
        }
    }

    public override void OnUseCard(BattlePlayingCardDataInUnitModel card)
    {
        this._using = card;
    }

    public override void OnWinParrying(BattleDiceBehavior behavior)
    {
        if (behavior.card != this._using)
        {
            return;
        }

        if (!behavior.card.card.HasBuf<PassiveAbility_Prescript.BattleDiceCardBuf_IndexMark>())
        {
            return;
        }

        int targetCost = ItemXmlDataList.instance.GetCardItem(behavior.TargetDice.card.card.GetID()).Spec.Cost;
        int selfCost = ItemXmlDataList.instance.GetCardItem(behavior.card.card.GetID()).Spec.Cost;

        if (targetCost > selfCost)
        {
            this._winnedParry = true;
        }
    }

    public override void OnSuccessAttack(BattleDiceBehavior behavior)
    {
        if (behavior.card != this._using)
        {
            return;
        }

        if (!behavior.card.card.HasBuf<PassiveAbility_Prescript.BattleDiceCardBuf_IndexMark>())
        {
            return;
        }

        if (this._winnedParry && behavior.TargetDice == null)
        {
            this.IsPassed = true;

            if (behavior.card.target.bufListDetail.HasBuf<BattleUnitBuf_TargetOfPrescript>())
            {
                this.IsPassedByTarget = true;
            }
        }
    }

    public override void OnEndBattle(BattlePlayingCardDataInUnitModel curCard)
    {
        this._using = null;
        this._winnedParry = false;
    }

    public override bool IsIndexMarkNeeds(BattleDiceCardModel model)
    {
        if (base._owner != null && base._owner.passiveDetail.HasPassive<PassiveAbility_Esther>())
        {
            LorId[] pids = new[] { new LorId(PowerfulEstherMOD.packageId, 27) };

            return pids.Contains(model.GetID());
        }

        return ItemXmlDataList.instance.GetCardItem(model.GetID()).DiceBehaviourList.FindAll(dbh => dbh.Type != BehaviourType.Standby).Count != 1 && base.SetIndexMarkForAtkDice(model);
    }

    public override bool IsSelectable(PassiveAbilityBase self)
    {
        bool foundBiggerOneDice = self.Owner.allyCardDetail.GetHand().Exists(hand => ItemXmlDataList.instance.GetCardItem(hand.GetID()).DiceBehaviourList.FindAll(dbh => dbh.Type != BehaviourType.Standby).Count >= 2);

        return !foundBiggerOneDice && base.SelectAtkDiceNeeds(self);
    }

    private BattlePlayingCardDataInUnitModel _using;

    private bool _winnedParry = false;
}
