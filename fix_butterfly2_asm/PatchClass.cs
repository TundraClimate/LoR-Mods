using HarmonyLib;

namespace Butterfly2FixMod
{
    public class PatchClass
    {
        [HarmonyPatch(typeof(EmotionCardAbility_butterfly2.BattleUnitBuf_Emotion_Butterfly_DmgByDebuf), "GetDamageReduction")]
        public class PrefixPatch_Butterfly2
        {
            public static bool Prefix(EmotionCardAbility_butterfly2.BattleUnitBuf_Emotion_Butterfly_DmgByDebuf __instance, ref int __result, BattleUnitModel ____owner, BattleDiceBehavior behavior)
            {
                if (____owner.bufListDetail.GetNegativeBufTypeCount() > 0)
                {
                    __result = -RandomUtil.Range(2, 7);
                }
                __result = 0;

                return false;
            }
        }
    }
}
