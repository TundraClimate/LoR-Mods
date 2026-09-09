public class DiceCardAbility_ExtremeBoris_BorisPrepare : UnbreakableDice
{
    public override string[] Keywords => ["ExtremeBoris_UnbreakableDice"];

    public override void OnSucceedAttack()
    {
        var dmgUp = base.owner.bufListDetail.GetActivatedBuf(KeywordBuf.DmgUp);

        if (dmgUp is not null)
        {
            base.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.DmgUp, dmgUp.stack, base.owner);
        }
    }
}
