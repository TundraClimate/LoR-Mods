public class DiceCardAbility_TheBleed : DiceCardAbilityBase
{
    public static string Desc = "自分に出血が3以上あるなら威力が5増加";

    public override void BeforeRollDice()
    {
        if (base.owner != null)
        {
            BattleUnitBuf bleed = base.owner.bufListDetail.GetActivatedBuf(KeywordBuf.Bleeding);

            if (bleed != null && bleed.stack >= 3)
            {
                base.behavior.ApplyDiceStatBonus(new DiceStatBonus()
                {
                    power = 5,
                });
            }
        }
    }
}
