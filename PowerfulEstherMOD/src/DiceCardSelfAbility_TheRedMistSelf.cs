public class DiceCardSelfAbility_TheRedMistSelf : DiceCardSelfAbilityBase
{
    public static string Desc = "[使用時] 全ダイス威力が自身の指令の加護の値だけ増加";

    public override void OnUseCard()
    {
        if (base.owner != null && base.card != null)
        {
            BattleUnitBuf grace = base.owner.bufListDetail.GetActivatedBufList().Find(buf => buf is BattleUnitBuf_GraceOfPrescript);

            if (grace != null)
            {
                base.card.ApplyDiceStatBonus(_ => true, new DiceStatBonus()
                {
                    power = grace.stack,
                });
            }
        }
    }
}
