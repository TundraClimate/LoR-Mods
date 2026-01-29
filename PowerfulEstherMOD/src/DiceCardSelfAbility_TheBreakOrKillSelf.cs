public class DiceCardSelfAbility_TheBreakOrKillSelf : DiceCardSelfAbilityBase
{
    public static string Desc = "[戦闘開始時] この幕の間、与える混乱ダメージ量が8増加。";

    public override void OnStartBattle()
    {
        if (base.owner != null)
        {
            base.owner.bufListDetail.AddBuf(new BattleUnitBuf_BreakDmgAddr());
        }
    }

    private class BattleUnitBuf_BreakDmgAddr : BattleUnitBuf
    {
        public override bool Hide
        {
            get
            {
                return true;
            }
        }

        public override void OnRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(new DiceStatBonus()
            {
                breakDmg = 8,
            });
        }

        public override void OnRoundEnd()
        {
            base.Destroy();
        }
    }
}
