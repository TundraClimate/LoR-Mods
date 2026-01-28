using LOR_DiceSystem;

public class DiceCardAbility_TheCounter : DiceCardAbilityBase
{
    public static string Desc = "[マッチ勝利時] 打撃ダメージを8与える";

    public override void OnWinParrying()
    {
        BattleUnitModel target = base.behavior.card.target;

        if (target == null)
        {
            return;
        }

        int dmg = 8;

        switch (target.GetResistHP(BehaviourDetail.Hit))
        {
            case AtkResist.Vulnerable:
                dmg = dmg * 2;

                break;
            case AtkResist.Weak:
                dmg = (int)(dmg * 1.5);

                break;
            case AtkResist.Normal:
                break;
            case AtkResist.Endure:
                dmg = (int)(dmg * 0.5);

                break;
            case AtkResist.Resist:
                dmg = (int)(dmg * 0.25);

                break;
            case AtkResist.Immune:
            default:
                dmg = (int)(dmg * 0);

                break;
        }

        UnityEngine.Debug.Log(target.GetResistHP(BehaviourDetail.Hit) + ": " + dmg);

        target.TakeDamage(dmg, DamageType.Card_Ability, base.owner);
    }
}
