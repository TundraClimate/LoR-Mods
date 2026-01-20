public class BattleUnitBuf_TheUseMarked : PrescriptBuf
{
    protected override string keywordId
    {
        get
        {
            return "TheUseMarked";
        }
    }

    public override void OnUseCard(BattlePlayingCardDataInUnitModel card)
    {
        if (card.card.HasBuf<PassiveAbility_Prescript.BattleDiceCardBuf_IndexMark>())
        {
            this.IsPassed = true;

            if (card.target.bufListDetail.HasBuf<BattleUnitBuf_TargetOfPrescript>())
            {
                this.IsPassedByTarget = true;
            }
        }
    }
}
