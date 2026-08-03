using HarmonyExtension;

public class DiceCardSelfAbility_UltraSandBagMOD_BoostNegative : SpecialCardBase
{
    public override void OnClick(BattleUnitModel owner)
    {
        owner.emotionDetail.AllEmotionCoins
            .Add((EmotionCoin)typeof(EmotionCoin).Ctor([typeof(EmotionCoinType)]).Invoke([EmotionCoinType.Negative]));

        typeof(BattleUnitEmotionDetail).Method("LevelUp").Invoke(owner.emotionDetail, []);

        BattleObjectManager.instance.InitUI();
    }
}
