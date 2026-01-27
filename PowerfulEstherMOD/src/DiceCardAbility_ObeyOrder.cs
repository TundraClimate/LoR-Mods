public class DiceCardAbility_ObeyOrder : DiceCardAbilityBase
{
    public static string Desc = "[マッチ勝利時] 指令の印が刻まれているなら、次の幕にパワーか忍耐かクイックを1得る。";

    public override void OnWinParrying()
    {
        BattlePlayingCardDataInUnitModel currentPage = base.owner.currentDiceAction;

        if (currentPage == null)
        {
            return;
        }

        if (base.owner == null || !currentPage.card.HasBuf<PassiveAbility_Prescript.BattleDiceCardBuf_IndexMark>())
        {
            return;
        }

        int elected = RandomUtil.Range(1, 3);

        switch (elected)
        {
            case 1:
                base.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Strength, 1, base.owner);

                break;
            case 2:
                base.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Endurance, 1, base.owner);

                break;
            case 3:
                base.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 1, base.owner);

                break;
        }
    }
}
