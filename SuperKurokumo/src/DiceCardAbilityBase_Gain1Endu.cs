public class DiceCardSelfAbility_Gain1Endu : AdvancedCardBase
{
    public static string Desc = "[戦闘開始時] 忍耐1を得る";

    public override void OnStartBattle()
    {
        base.owner?.bufListDetail?.AddKeywordBufThisRoundByCard(KeywordBuf.Endurance, 1, base.owner);
    }
}
