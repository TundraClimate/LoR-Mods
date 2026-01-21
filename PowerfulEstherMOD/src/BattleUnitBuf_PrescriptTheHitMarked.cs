public class BattleUnitBuf_TheHitMarked : PrescriptBuf
{
    protected override string keywordId
    {
        get
        {
            return "TheHitMarked";
        }
    }

    public override void OnSuccessAttack(BattleDiceBehavior behavior)
    {
        if (behavior.card.card.HasBuf<PassiveAbility_Prescript.BattleDiceCardBuf_IndexMark>())
        {
            this.IsPassed = true;

            if (behavior.card.target.bufListDetail.HasBuf<BattleUnitBuf_TargetOfPrescript>())
            {
                this.IsPassedByTarget = true;
            }
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
