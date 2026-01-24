using LOR_DiceSystem;

public class BattleUnitBuf_TheNormalResist : PrescriptBuf
{
    protected override string keywordId
    {
        get
        {
            return "TheNormalResist";
        }
    }

    public override void OnSuccessAttack(BattleDiceBehavior behavior)
    {
        BehaviourDetail atkDit = behavior.Detail;
        AtkResist resist = behavior.card.target.GetResistHP(atkDit);

        switch (resist)
        {
            case AtkResist.Weak:
            case AtkResist.Vulnerable:
            case AtkResist.Normal:

                this.IsPassed = true;

                if (behavior.card.card.HasBuf<PassiveAbility_Prescript.BattleDiceCardBuf_IndexMark>() || behavior.card.target.bufListDetail.HasBuf<BattleUnitBuf_TargetOfPrescript>())
                {
                    this.IsPassedByTarget = true;
                }

                break;
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
}
