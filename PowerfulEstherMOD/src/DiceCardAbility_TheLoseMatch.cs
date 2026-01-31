public class DiceCardAbility_TheLoseMatch : DiceCardAbilityBase
{
    public static string Desc = "[マッチ敗北時] 次の幕にクイックを3得る。";

    public override void OnLoseParrying()
    {
        if (base.owner != null)
        {
            base.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 3, base.owner);
        }
    }
}
