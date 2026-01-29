public class DiceCardAbility_TheBreakOrKillD2 : DiceCardAbilityBase
{
    public static string Desc = "[マッチ勝利時] 相手に3混乱ダメージ";

    public override void OnWinParrying()
    {
        if (base.owner == null || base.owner.currentDiceAction.target == null)
        {
            return;
        }

        base.owner.currentDiceAction.target.TakeBreakDamage(3, DamageType.Card_Ability, base.owner);
    }
}
