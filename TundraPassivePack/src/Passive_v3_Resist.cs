using LOR_DiceSystem;

public class PassiveAbility_TundraPassivePack_Resist : AdvancedPassiveBase
{
    public override AtkResist GetResistHP(AtkResist origin, BehaviourDetail detail)
    {
        return AtkResist.Resist;
    }

    public override AtkResist GetResistBP(AtkResist origin, BehaviourDetail detail)
    {
        return AtkResist.Resist;
    }
}
