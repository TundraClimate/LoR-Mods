using System.Collections.Generic;
using LOR_DiceSystem;

public class BattleUnitBuf_ThePenetrate : PrescriptBuf
{
    protected override string keywordId
    {
        get
        {
            return "ThePenetrate";
        }
    }

    public override void OnSuccessAttack(BattleDiceBehavior behavior)
    {
        if (behavior.card.card.HasBuf<PassiveAbility_Prescript.BattleDiceCardBuf_IndexMark>())
        {
            if (behavior.Detail == BehaviourDetail.Penetrate)
            {
                this._totalHit++;
            }

            if (behavior.TargetDice.owner.bufListDetail.HasBuf<BattleUnitBuf_TargetOfPrescript>())
            {
                this._totalHitByTarget++;
            }

            if (this._totalHit >= 2)
            {
                this.IsPassed = true;

                if (this._totalHitByTarget >= 2)
                {
                    this.IsPassedByTarget = true;
                }
            }
        }
    }

    public override bool IsIndexMarkNeeds(BattleDiceCardModel model)
    {
        List<DiceBehaviour> behs = ItemXmlDataList.instance.GetCardItem(model.GetID()).DiceBehaviourList;
        int count = 0;

        foreach (DiceBehaviour beh in behs)
        {
            if (beh.Type == BehaviourType.Atk && beh.Detail == BehaviourDetail.Penetrate)
            {
                count++;
            }
        }

        return count >= 2;
    }

    public override bool IsSelectable(PassiveAbilityBase self)
    {
        List<BattleDiceCardModel> hands = self.Owner.allyCardDetail.GetHand();
        int count = 0;

        foreach (BattleDiceCardModel hand in hands)
        {
            List<DiceBehaviour> behs = ItemXmlDataList.instance.GetCardItem(hand.GetID()).DiceBehaviourList;

            foreach (DiceBehaviour beh in behs)
            {
                if (beh.Type == BehaviourType.Atk && beh.Detail == BehaviourDetail.Penetrate)
                {
                    count++;
                }
            }

            if (count >= 2)
            {
                return true;
            }
        }

        return false;
    }

    private int _totalHit = 0;

    private int _totalHitByTarget = 0;
}
