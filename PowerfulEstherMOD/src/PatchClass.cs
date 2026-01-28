using System.Collections.Generic;
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

    [HarmonyPatch(typeof(BattleKeepedCardDataInUnitModel), "AddBehaviours", new[] { typeof(BattleDiceCardModel), typeof(List<BattleDiceBehavior>) })]
    public class PrefixPatch_AddBehaviours
    {
        public static void Prefix(BattleDiceCardModel card, List<BattleDiceBehavior> behaviourList)
        {
            foreach (BattleDiceBehavior beh in behaviourList)
            {
                if (beh.card.card != null && beh.card.card.HasBuf<PassiveAbility_Prescript.BattleDiceCardBuf_IndexMark>())
                {
                    beh.abilityList.Add(new PassiveAbility_Prescript.DiceCardAbility_Marker());
                }
            }
        }
    }
}
