public class BattleUnitBuf_TargetOfPrescript : BattleUnitBuf
{
    protected override string keywordId
    {
        get
        {
            return "TargetOfPrescript";
        }
    }

    public BattleUnitBuf_TargetOfPrescript()
    {
        base.stack = 0;
    }

    public override void OnRoundEnd()
    {
        base.Destroy();
    }
}
