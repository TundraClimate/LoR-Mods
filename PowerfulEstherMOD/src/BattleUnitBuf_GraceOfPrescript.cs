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

    public void AddStack(int stack = 1)
    {
        base.stack += stack;
    }
}
