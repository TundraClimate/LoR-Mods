using HarmonyLib;

public static class PatchClass
{
    [HarmonyPatch(typeof(BattleDiceCardBuf), "GetBufIcon")]
    public class PostfixPatch_GetCardBufIcon
    {
        public static void Postfix(ref UnityEngine.Sprite __result, BattleDiceCardBuf __instance)
        {
            if (__result == null && BattleUnitBuf._bufIconDictionary != null)
            {
                string keywordIconId = (string)(AccessTools.Property(typeof(BattleDiceCardBuf), "keywordIconId").GetValue(__instance));

                BattleUnitBuf._bufIconDictionary.TryGetValue(keywordIconId, out __result);
            }
        }
    }
}
