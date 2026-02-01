using LOR_DiceSystem;

public class DiceCardAbility_TheNormalResist : DiceCardAbilityBase
{
    public static string Desc = "[的中時] 相手の耐性を脆弱としてダメージを与える。";

    public override void BeforeGiveDamage(BattleUnitModel target)
    {
        if (target != null)
        {
            target.bufListDetail.AddBuf(new BattleUnitBuf_WeakDamage());
        }
    }

    private class BattleUnitBuf_WeakDamage : BattleUnitBuf
    {
        public override bool Hide
        {
            get
            {
                return true;
            }
        }

        public override AtkResist GetResistHP(AtkResist origin, BehaviourDetail detail)
        {
            return AtkResist.Weak;
        }

        public override void OnTakeDamageByAttack(BattleDiceBehavior atkDice, int dmg)
        {
            if (base._owner != null)
            {
                base._owner.bufListDetail.RemoveBuf(this);
            }
        }
    }
}
