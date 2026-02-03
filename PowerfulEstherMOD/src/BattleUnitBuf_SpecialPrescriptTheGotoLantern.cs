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

    public override bool ShouldSendScript()
    {
        List<BattleUnitModel> alives = BattleObjectManager.instance.GetAliveList(Faction.Player);

        return alives.Exists(unit => unit.emotionDetail.GetSelectedCardList().Find(emo => emo.AbilityList.Exists(abi => abi is EmotionCardAbility_bigbird2)) != null);
    }
}
