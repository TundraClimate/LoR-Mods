public class PassiveAbility_TundraPassivePack_HeavyBattery : AdvancedPassiveBase
{
    public override int SpeedDiceBreakedAdder()
    {
        return 1;
    }

    public override int GetSpeedDiceAdder(int speedDiceResult)
    {
        return -100;
    }
}
