public class DiceCardSelfAbility_ExtremeBoris_Respect : AdvancedCardBase
{
    public override void OnStartBattle()
    {
        foreach (BattleUnitModel item in BattleObjectManager.instance.GetAliveList_random(base.owner.faction, 2))
        {
            item.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.DmgUp, 2, base.owner);
        }
    }
}
