using System.Collections.Generic;
using LOR_DiceSystem;

public class BattleUnitBuf_TheCounter : PrescriptBuf
{
    protected override string keywordId
    {
        get
        {
            return "TheCounter";
        }
    }

    public override void OnWinParrying(BattleDiceBehavior behavior)
    {
        if (behavior.Detail != BehaviourDetail.Guard || behavior.TargetDice.card.card.GetSpec().Ranged != CardRange.Near)
        {
            return;
        }

        if (!this._usedModels.Contains(behavior.card))
        {
            this._usedModels.Add(behavior.card);
        }

        if (behavior.TargetDice.owner.bufListDetail.HasBuf<BattleUnitBuf_TargetOfPrescript>())
        {
            this._isPassedByTarget = true;
        }

        if (behavior.card.card.HasBuf<PassiveAbility_Prescript.BattleDiceCardBuf_IndexMark>() || this._usedModels.Count >= 3)
        {
            this.IsPassed = true;
            this.IsPassedByTarget = this._isPassedByTarget;
        }
    }

    public override bool IsIndexMarkNeeds(BattleDiceCardModel model)
    {
        if (base._owner != null && base._owner.passiveDetail.HasPassive<PassiveAbility_Esther>())
        {
            LorId[] pids = new[] { new LorId(PowerfulEstherMOD.packageId, 14) };

            return pids.Contains(model.GetID());
        }

        return ItemXmlDataList.instance.GetCardItem(model.GetID()).DiceBehaviourList.Exists(dice =>
            dice.Detail == BehaviourDetail.Guard
        );
    }

    public override bool IsSelectable(PassiveAbilityBase self)
    {
        List<BattleDiceCardModel> hands = self.Owner.allyCardDetail.GetHand();

        bool hasGuardDice = hands.Exists(hand =>
                ItemXmlDataList.instance.GetCardItem(hand.GetID()).DiceBehaviourList.Exists(dice =>
                    dice.Detail == BehaviourDetail.Guard
                )
            );

        return hasGuardDice;
    }

    private List<BattlePlayingCardDataInUnitModel> _usedModels = new List<BattlePlayingCardDataInUnitModel>();

    private bool _isPassedByTarget = false;
}
