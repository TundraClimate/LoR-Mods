public class BattleUnitBuf_Unlock3 : BattleUnitBuf
{
    protected override string keywordId
    {
        get
        {
            return "Unlock3";
        }
    }

    public BattleUnitBuf_Unlock3()
    {
        base.stack = 0;
    }

    public void Lock()
    {
        base._owner.bufListDetail.RemoveBuf(this);
    }
}
