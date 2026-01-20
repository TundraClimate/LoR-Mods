public class PrescriptBuf : BattleUnitBuf
{
    public void Init()
    {
        base.stack = 0;
    }

    public static PrescriptBuf Create<T>(T instance)
        where T : PrescriptBuf
    {
        PrescriptBuf prescript = (PrescriptBuf)instance;

        prescript.Init();

        return prescript;
    }

    public virtual bool IsPassed
    {
        get
        {
            return true;
        }
    }

    public virtual bool IsPassedByTarget
    {
        get
        {
            return this.IsPassed && false;
        }
    }

    public override void OnRoundEnd()
    {
        base.Destroy();
    }
}
