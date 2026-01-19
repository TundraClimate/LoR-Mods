public class BattleUnitBuf_Unlock : BattleUnitBuf
{
    protected override string keywordId
    {
        get
        {
            return "Unlock";
        }
    }

    public BattleUnitBuf_Unlock()
    {
        base.stack = 0;
    }

    public void Lock()
    {
        base._owner.bufListDetail.RemoveBuf(this);
    }
}
