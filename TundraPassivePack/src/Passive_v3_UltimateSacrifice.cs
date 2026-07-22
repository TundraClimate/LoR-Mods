using HarmonyExtension;

public class PassiveAbility_TundraPassivePack_UltimateSacrifice : AdvancedPassiveBase
{
    public override void OnWaveStartBefore()
    {
        base.owner.DieNoPatch();

        base.owner.faction.AliveUnits.Filter(unit => unit != base.owner).Map(unit => unit.emotionDetail).Foreach(det =>
        {
            det.AllEmotionCoins.Add((EmotionCoin)typeof(EmotionCoin).Ctor([typeof(EmotionCoinType)]).Invoke([EmotionCoinType.Negative]));

            typeof(BattleUnitEmotionDetail).Method("LevelUp").Invoke(det, []);
        });
    }
}
