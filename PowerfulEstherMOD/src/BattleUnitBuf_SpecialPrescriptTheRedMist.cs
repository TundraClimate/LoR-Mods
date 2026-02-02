using System.Collections.Generic;

public class BattleUnitBuf_TheRedMist : SpecialPrescriptBuf
{
    protected override string keywordId
    {
        get
        {
            return "TheRedMist";
        }
    }

    public override int SpeedDiceNumAdder()
    {
        return 0;
    }

    public override bool IsIndexMarkNeeds(BattleDiceCardModel model)
    {
        if (base._owner != null && base._owner.passiveDetail.HasPassive<PassiveAbility_Esther>())
        {
            LorId[] pids = new[] { new LorId(PowerfulEstherMOD.packageId, 99) };

            return pids.Contains(model.GetID());
        }

        return base.IsIndexMarkNeeds(model);
    }

    public override bool ShouldSendScript()
    {
        List<BattleUnitModel> alives = BattleObjectManager.instance.GetAliveList(Faction.Player);

        return alives.Exists(unit => unit.bufListDetail.GetActivatedBuf(KeywordBuf.RedMistEgo) != null);
    }

    public override List<LorId> InitializeHand()
    {
        return new List<LorId>()
        {
            new LorId(PowerfulEstherMOD.packageId, 99),
        };
    }

    public override BattleUnitModel FixedIndexTarget(List<BattleUnitModel> candidates, BattleUnitModel origin)
    {
        foreach (BattleUnitModel candidate in candidates)
        {
            if (candidate.bufListDetail.GetActivatedBuf(KeywordBuf.RedMistEgo) != null)
            {
                return candidate;
            }
        }

        return base.FixedIndexTarget(candidates, origin);
    }
}
