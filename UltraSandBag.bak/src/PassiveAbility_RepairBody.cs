public class PassiveAbility_RepairBody : PassiveAbilityBase
{
    public override void OnRoundStart()
    {
        if (base.owner == null)
        {
            return;
        }

        base.owner.RecoverHP(99999);
        this._reducateDmg = 0;
    }

    public override bool BeforeTakeDamage(BattleUnitModel attacker, int dmg)
    {
        if (dmg >= (int)base.owner.hp)
        {
            this._reducateDmg = dmg;
        }

        return base.BeforeTakeDamage(attacker, dmg);
    }

    public override int GetDamageReductionAll()
    {
        if (this._reducateDmg != 0)
        {
            return this._reducateDmg;
        }

        return base.GetDamageReductionAll();
    }

    public override int GetBreakDamageReductionAll(int dmg, DamageType dmgType, BattleUnitModel attacker)
    {
        return dmg;
    }

    private int _reducateDmg;
}
