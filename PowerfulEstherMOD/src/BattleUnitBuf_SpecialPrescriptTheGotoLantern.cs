using System.Collections.Generic;

public class BattleUnitBuf_TheGotoLantern : SpecialPrescriptBuf
{
    protected override string keywordId
    {
        get
        {
            return "TheGotoLantern";
        }
    }

    public override void OnUseCard(BattlePlayingCardDataInUnitModel card)
    {
        if (card.target != null && card.target.bufListDetail.HasBuf<BattleUnitBuf_TargetOfPrescript>())
        {
            base.IsPassedByTarget = true;
        }
    }

    public override int SpeedDiceNumAdder()
    {
        if (base._owner.emotionDetail.EmotionLevel > 3)
        {
            return 2;
        }

        return 3;
    }

    public override bool IsIndexMarkNeeds(BattleDiceCardModel model)
    {
        if (base._owner != null && base._owner.passiveDetail.HasPassive<PassiveAbility_Esther>())
        {
            LorId[] pids = new[] { new LorId(PowerfulEstherMOD.packageId, 98) };

            return pids.Contains(model.GetID());
        }

        return base.IsIndexMarkNeeds(model);
    }

    public override List<LorId> InitializeHand()
    {
        return new List<LorId>()
        {
            new LorId(PowerfulEstherMOD.packageId, 98),
            new LorId(PowerfulEstherMOD.packageId, 98),
            new LorId(PowerfulEstherMOD.packageId, 98),
            new LorId(PowerfulEstherMOD.packageId, 98),
        };
    }

    public override bool ShouldSendScript()
    {
        List<BattleUnitModel> alives = BattleObjectManager.instance.GetAliveList(Faction.Player);

        return alives.Exists(unit => unit.emotionDetail.GetSelectedCardList().Find(emo => emo.AbilityList.Exists(abi => abi is EmotionCardAbility_bigbird2)) != null);
    }

    public override BattleUnitModel FixedIndexTarget(List<BattleUnitModel> candidates, BattleUnitModel origin)
    {
        BattleUnitModel lantern = candidates.Find(candidate => candidate.emotionDetail.GetSelectedCardList().Find(emo => emo.AbilityList.Exists(abi => abi is EmotionCardAbility_bigbird2)) != null);

        if (lantern != null)
        {
            return lantern;
        }

        return base.FixedIndexTarget(candidates, origin);
    }
}
