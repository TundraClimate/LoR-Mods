public class DiceCardSelfAbility_BoostPositive : DiceCardSelfAbilityBase
{
    public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
    {
        if (unit == null)
        {
            return;
        }

        int count = unit.emotionDetail.GetAccumulatedEmotionCoinNum() + unit.emotionDetail.MaximumCoinNumber - unit.emotionDetail.totalEmotionCoins.Count;

        if (count <= 0)
        {
            return;
        }

        unit.emotionDetail.CreateEmotionCoin(EmotionCoinType.Positive, count);
    }
}
