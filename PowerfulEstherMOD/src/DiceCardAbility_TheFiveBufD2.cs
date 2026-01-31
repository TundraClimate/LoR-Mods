public class DiceCardAbility_TheFiveBufD2 : DiceCardAbilityBase
{
    public static string Desc = "[マッチ勝利時] 次の幕に出血を2付与";

    public override void OnWinParrying()
    {
        if (base.behavior.card.target != null)
        {
            base.behavior.card.target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Bleeding, 2, base.owner);
        }
    }
}
