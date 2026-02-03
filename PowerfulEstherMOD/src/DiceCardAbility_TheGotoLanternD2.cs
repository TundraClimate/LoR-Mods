public class DiceCardAbility_TheGotoLanternD2 : DiceCardAbilityBase
{
    public static string Desc = "[マッチ勝利時] 相手に7ダメージ。";

    public override void OnWinParrying()
    {
        if (base.card != null && base.card.target != null)
        {
            base.card.target.TakeDamage(7, DamageType.Card_Ability, base.owner);
        }
    }
}
