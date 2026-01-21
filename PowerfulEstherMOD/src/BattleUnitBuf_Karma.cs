using System;

public class BattleUnitBuf_Karma : BattleUnitBuf
{
    protected override string keywordId
    {
        get
        {
            return "Karma";
        }
    }

    public BattleUnitBuf_Karma()
    {
        base.stack = 1;
    }

    public BattleUnitBuf_Karma(int stack)
    {
        base.stack = stack;
    }

    public void AddStack(int stack = 1)
    {
        base.stack = Math.Min(base.stack + stack, 10);
    }
}
