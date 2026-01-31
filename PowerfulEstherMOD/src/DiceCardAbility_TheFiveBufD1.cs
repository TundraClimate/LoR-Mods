public class DiceCardAbility_TheFiveBufD1 : DiceCardAbilityBase
{
    public static string Desc = "[マッチ勝利時] 次の幕に出血を3付与";

    public override void OnWinParrying()
    {
        if (base.behavior.card.target != null)
        {
            base.behavior.card.target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Bleeding, 3, base.owner);
        }
    }
}
