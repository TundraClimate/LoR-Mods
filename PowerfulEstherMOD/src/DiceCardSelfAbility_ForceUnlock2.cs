public class DiceCardSelfAbility_ForceUnlock2 : DiceCardSelfAbilityBase
{
    public static string Desc = "幕の終了時、指令の加護が6未満なら6まで増加。増加した値だけカルマを得る";

    public override void OnUseCard()
    {
        base.owner.bufListDetail.AddBuf(new BattleUnitBuf_ForceUnlock2Lazy());
    }

    private class BattleUnitBuf_ForceUnlock2Lazy : BattleUnitBuf
    {
        public override bool Hide
        {
            get
            {
                return true;
            }
        }

        public override void OnRoundEndTheLast()
        {
            base.Destroy();

            BattleUnitBuf_GraceOfPrescript grace = (BattleUnitBuf_GraceOfPrescript)base._owner.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf buf) => buf is BattleUnitBuf_GraceOfPrescript);

            if (grace == null)
            {
                return;
            }

            if (grace.stack >= 6)
            {
                return;
            }

            int needs = 6 - grace.stack;

            grace.AddStack(needs);

            BattleUnitBuf_Karma karma = (BattleUnitBuf_Karma)base._owner.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf buf) => buf is BattleUnitBuf_Karma);

            if (karma == null)
            {
                karma = new BattleUnitBuf_Karma(needs);

                base._owner.bufListDetail.AddBuf(karma);
            }
            else
            {
                karma.AddStack(needs);
            }
        }
    }
}
