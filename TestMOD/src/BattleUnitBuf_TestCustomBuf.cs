using DeviceOfHermes.AdvancedBase;

public class BattleUnitBuf_TestCustomBuf : AdvancedUnitBuf
{
    public override int DefaultStack => 4;

    protected override string keywordId
    {
        get
        {
            return "TestCustomBuf";
        }
    }

    public override void OnStackChange(int last)
    {
        Hermes.Say($"Stack changed {last} => {base.stack}");
    }

    public override void OnRoundEnd()
    {
        base.stack -= 1;

        if (base.stack == 0)
        {
            base.Destroy();
        }
    }
}
