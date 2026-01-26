using System;

public class BattleUnitBuf_GraceOfPrescript : BattleUnitBuf
{
    protected override string keywordId
    {
        get
        {
            return "GraceOfPrescript";
        }
    }

    public BattleUnitBuf_GraceOfPrescript()
    {
        base.stack = 0;
    }

    public override void BeforeRollDice(BattleDiceBehavior behavior)
    {
        int stack = base.stack;

        behavior.ApplyDiceStatBonus(new DiceStatBonus()
        {
            power = stack / 3,
            dmg = stack / 6,
            breakDmg = stack / 9,
        });
    }

    public void AddStack(int stack = 1)
    {
        base.stack = Math.Min(base.stack + stack, 9);
    }
}
