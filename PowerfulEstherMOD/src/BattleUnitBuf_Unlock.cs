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

    public override void OnRoundEnd()
    {
        if (!base._owner.IsDead())
        {
            base._owner.breakDetail.RecoverBreak(5);
        }
    }

    public void Lock()
    {
        base._owner.bufListDetail.RemoveBuf(this);
    }
}
