public class DiceCardSelfAbility_TheBleedSelf : DiceCardSelfAbilityBase
{
    public static string Desc = "[使用時] 出血8を得る";

    public override void OnUseCard()
    {
        if (base.owner != null)
        {
            base.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Bleeding, 8);
        }
    }
}
