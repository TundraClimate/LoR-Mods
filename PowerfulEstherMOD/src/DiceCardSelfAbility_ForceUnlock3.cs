public class DiceCardSelfAbility_ForceUnlock3 : DiceCardSelfAbilityBase
{
    public static string Desc = "幕の終了時、指令の加護が9未満なら9まで増加。増加した値だけカルマを得る";

    public override void OnUseCard()
    {
        base.owner.bufListDetail.AddBuf(new BattleUnitBuf_ForceUnlock3Lazy());
    }

    private class BattleUnitBuf_ForceUnlock3Lazy : BattleUnitBuf
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

            if (grace.stack >= 9)
            {
                return;
            }

            int needs = 9 - grace.stack;

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
