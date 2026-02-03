public class DiceCardSelfAbility_TheGotoLanternSelf : DiceCardSelfAbilityBase
{
    public static string Desc = "[使用時] 次の幕にクイック1を得る。";

    public override void OnUseCard()
    {
        if (base.owner != null)
        {
            base.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 1, base.owner);
        }
    }
}
