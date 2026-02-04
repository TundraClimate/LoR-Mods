public class DiceCardSelfAbility_TheKillPinocchioSelf : DiceCardSelfAbilityBase
{
    public static string Desc = @"このページのダイスは威力を2倍に受ける。
[使用時] 全ダイス威力が4増加。";

    public override void OnStartBattleAfterCreateBehaviour()
    {
        if (base.card != null)
        {
            base.card.ApplyDiceAbility(_ => true, new DiceCardAbility_powerDouble());
        }
    }

    public override void OnUseCard()
    {
        if (base.card != null)
        {
            base.card.ApplyDiceStatBonus(_ => true, new DiceStatBonus()
            {
                power = 4,
            });
        }
    }
}
