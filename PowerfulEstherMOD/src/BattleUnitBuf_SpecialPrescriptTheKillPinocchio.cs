using System.Collections.Generic;

public class BattleUnitBuf_TheKillPinocchio : SpecialPrescriptBuf
{
    protected override string keywordId
    {
        get
        {
            return "TheKillPinocchio";
        }
    }

    public override void OnKill(BattleUnitModel target)
    {
        if (target != null && target.emotionDetail.GetSelectedCardList().Exists(emo => emo.AbilityList.Exists(abi => abi is EmotionCardAbility_pinocchio1)))
        {
            base.IsPassedByTarget = true;
        }
    }

    public override int SpeedDiceNumAdder()
    {
        if (base._owner.emotionDetail.EmotionLevel > 3)
        {
            return 0;
        }

        return 1;
    }

    public override bool IsIndexMarkNeeds(BattleDiceCardModel model)
    {
        if (base._owner != null && base._owner.passiveDetail.HasPassive<PassiveAbility_Esther>())
        {
            LorId[] pids = new[] { new LorId(PowerfulEstherMOD.packageId, 97) };

            return pids.Contains(model.GetID());
        }

        return base.IsIndexMarkNeeds(model);
    }

    public override List<LorId> InitializeHand()
    {
        return new List<LorId>()
        {
            new LorId(PowerfulEstherMOD.packageId, 97),
            new LorId(PowerfulEstherMOD.packageId, 97),
        };
    }

    public override bool ShouldSendScript()
    {
        List<BattleUnitModel> alives = BattleObjectManager.instance.GetAliveList(Faction.Player);
        LorId lid = new LorId(1100001);
        LorId lid2 = new LorId(1100001);

        return alives.Exists(unit => unit.allyCardDetail.GetHand().Exists(card => card.GetID() == lid || card.GetID() == lid2));
    }

    public override BattleUnitModel FixedIndexTarget(List<BattleUnitModel> candidates, BattleUnitModel origin)
    {
        BattleUnitModel pinocchio = candidates.Find(candidate => candidate.emotionDetail.GetSelectedCardList().Exists(emo => emo.AbilityList.Exists(abi => abi is EmotionCardAbility_pinocchio1)));

        if (pinocchio != null)
        {
            return pinocchio;
        }

        return base.FixedIndexTarget(candidates, origin);
    }
}
