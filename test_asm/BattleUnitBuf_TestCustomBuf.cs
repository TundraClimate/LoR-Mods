public class BattleUnitBuf_TestCustomBuf : BattleUnitBuf
{
    public static string id
    {
        get
        {
            return string.Format("{0}_TestCustomBuf", TestMod.packageId);
        }
    }

    protected override string keywordId
    {
        get
        {
            return BattleUnitBuf_TestCustomBuf.id;
        }
    }

    public override void OnRoundEnd()
    {
        base.OnRoundEnd();
        base.Destroy();
    }
}
