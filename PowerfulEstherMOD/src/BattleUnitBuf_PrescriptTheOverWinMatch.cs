public class BattleUnitBuf_TheOverWinMatch : PrescriptBuf
{
    protected override string keywordId
    {
        get
        {
            return "TheOverWinMatch";
        }
    }

    public override void OnWinParrying(BattleDiceBehavior behavior)
    {
        if (!behavior.card.card.HasBuf<PassiveAbility_Prescript.BattleDiceCardBuf_IndexMark>() && !behavior.abilityList.Exists(abi => abi is PassiveAbility_Prescript.DiceCardAbility_Marker))
        {
            return;
        }

        if (behavior.DiceResultValue - behavior.TargetDice.DiceResultValue >= 5)
        {
            this.IsPassed = true;

            if (behavior.TargetDice.owner.bufListDetail.HasBuf<BattleUnitBuf_TargetOfPrescript>())
            {
                this.IsPassedByTarget = true;
            }
        }
    }

    public override bool IsIndexMarkNeeds(BattleDiceCardModel model)
    {
        if (base._owner != null && base._owner.passiveDetail.HasPassive<PassiveAbility_Esther>())
        {
            LorId[] pids = new[] { new LorId(PowerfulEstherMOD.packageId, 12) };

            return pids.Contains(model.GetID());
        }

        return base.IsIndexMarkNeeds(model);
    }
}
