public class BattleUnitBuf_TheTerminateAll : BattleUnitBuf
{
    protected override string keywordId
    {
        get
        {
            return "TheTerminateAll";
        }
    }

    public BattleUnitBuf_TheTerminateAll()
    {
        base.stack = 0;
    }

    public override void OnRoundEnd()
    {
        base.Destroy();
    }
}
