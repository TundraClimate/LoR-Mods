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

    public override void OnRoundEnd()
    {
        if (!base._owner.IsDead())
        {
            base._owner.breakDetail.RecoverBreak(10);
        }
    }

    public void Lock()
    {
        base._owner.bufListDetail.RemoveBuf(this);
    }
}
