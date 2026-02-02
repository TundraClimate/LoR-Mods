public class DiceCardSelfAbility_TheKillOrDamageSelf : DiceCardSelfAbilityBase
{
    public static string Desc = "[使用時] 相手の速度が自分より高いなら、威力が3増加し、相手に麻痺を3付与。";

    public override void OnUseCard()
    {
        if (base.owner == null || base.owner.currentDiceAction.target.currentDiceAction == null)
        {
            return;
        }

        int selfSpeed = base.owner.currentDiceAction.speedDiceResultValue;
        int targetSpeed = base.owner.currentDiceAction.target.currentDiceAction.speedDiceResultValue;

        if (targetSpeed > selfSpeed)
        {
            base.owner.currentDiceAction.ApplyDiceStatBonus(_ => true, new DiceStatBonus()
            {
                power = 3,
            });
            base.owner.currentDiceAction.target.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Paralysis, 3, base.owner);
        }
    }
}
