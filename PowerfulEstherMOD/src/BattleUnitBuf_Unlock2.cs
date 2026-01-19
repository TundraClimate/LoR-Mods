public class BattleUnitBuf_Unlock2 : BattleUnitBuf
{
    protected override string keywordId
    {
        get
        {
            return "Unlock2";
        }
    }

    public BattleUnitBuf_Unlock2()
    {
        base.stack = 0;
    }

    public void Lock()
    {
        base._owner.bufListDetail.RemoveBuf(this);
    }
}
