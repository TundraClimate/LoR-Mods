public class DiceCardAbility_TheRedMist : DiceCardAbilityBase
{
    public static string Desc = "[的中] <color=red>カルマ</color>を3得る。";

    public override void OnSucceedAttack()
    {
        if (base.owner != null)
        {
            BattleUnitBuf_Karma karma = (BattleUnitBuf_Karma)base.owner.bufListDetail.GetActivatedBufList().Find(buf => buf is BattleUnitBuf_Karma);

            if (karma == null)
            {
                karma = new BattleUnitBuf_Karma(0);

                base.owner.bufListDetail.AddBuf(karma);
            }

            karma.AddStack(3);
        }
    }
}
