public class PassiveAbility_UltraSandBagMOD_RepairBody : AdvancedPassiveBase
{
    public override bool isImmortal => true;

    public override void OnRoundStart()
    {
        base.owner?.RecoverHP(99999);
    }

    public override int GetBreakDamageReductionAll(int dmg, DamageType dmgType, BattleUnitModel attacker)
    {
        return dmg;
    }
}
