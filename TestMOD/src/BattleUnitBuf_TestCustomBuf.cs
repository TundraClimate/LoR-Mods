using DeviceOfHermes.AdvancedBase;

public class BattleUnitBuf_TestCustomBuf : AdvancedUnitBuf
{
    public override bool IsInstant => true;

    protected override string keywordId
    {
        get
        {
            return "TestCustomBuf";
        }
    }

    public override void Init(BattleUnitModel owner)
    {
        Hermes.Say("Init by TestCustomBuf");
    }

    public override void OnInstant()
    {
        Hermes.Say($"Hermes says: Haha, Instant the {keywordId} inflicted!");
    }

    public override void OnRoundEnd()
    {
    }
}
