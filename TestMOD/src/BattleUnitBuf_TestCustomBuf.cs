public class BattleUnitBuf_TestCustomBuf : BattleUnitBuf
{
    protected override string keywordId
    {
        get
        {
            return "TestCustomBuf";
        }
    }

    public override void OnRoundEnd()
    {
        base.OnRoundEnd();
        base.Destroy();
    }
}
