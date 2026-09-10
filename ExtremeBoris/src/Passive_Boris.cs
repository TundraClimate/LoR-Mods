public class PassiveAbility_ExtremeBoris_Boris : ShimPassiveBase
{
    public override void Init() => PackageId = ExtremeBoris.packageId;

    public override void ApplyPattern() => Patterns.ApplyNextPattern(this);

    PatternList Patterns = new(true)
    {
        new(2, 3, 3, 4, 3, 3),
        new(1, 4, 4, 3, 4, 4),
    };

    public override int SpeedDiceNumAdder()
    {
        if (StageController.Instance.RoundTurn == 1)
        {
            return 3;
        }

        return 4;
    }
}
