public class DiceCardAbility_TerminationDice : DiceCardAbilityBase
{
    public static string Desc = "[的中] 本来のダイスの値×7×(解禁段階)だけ、相手に追加でダメージと混乱ダメージを与える";

    public override void OnSucceedAttack()
    {
        if (base.owner == null || base.behavior == null || base.behavior.card == null || base.behavior.card.target == null)
        {
            return;
        }

        int diceVal = base.behavior.DiceVanillaValue;
        int unlockLevel = 0;

        foreach (BattleUnitBuf buf in base.owner.bufListDetail.GetActivatedBufList())
        {
            if (buf is BattleUnitBuf_Unlock)
            {
                unlockLevel = 1;
            }

            if (buf is BattleUnitBuf_Unlock2)
            {
                unlockLevel = 2;
            }

            if (buf is BattleUnitBuf_Unlock3)
            {
                unlockLevel = 3;
            }
        }

        BattleUnitModel target = base.behavior.card.target;
        int dmg = diceVal * 7 * unlockLevel;

        if (dmg != 0)
        {
            target.TakeBreakDamage(dmg, DamageType.Card_Ability, base.owner);
            target.TakeDamage(dmg, DamageType.Card_Ability, base.owner);
        }
    }
}
