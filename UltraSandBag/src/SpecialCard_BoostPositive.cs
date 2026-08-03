using HarmonyExtension;

public class DiceCardSelfAbility_UltraSandBagMOD_BoostPositive : SpecialCardBase
{
    public override void OnClick(BattleUnitModel owner)
    {
        owner.emotionDetail.AllEmotionCoins
            .Add((EmotionCoin)typeof(EmotionCoin).Ctor([typeof(EmotionCoinType)]).Invoke([EmotionCoinType.Positive]));

        typeof(BattleUnitEmotionDetail).Method("LevelUp").Invoke(owner.emotionDetail, []);

        BattleObjectManager.instance.InitUI();
    }
}
