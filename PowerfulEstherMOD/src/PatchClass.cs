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

    [HarmonyPatch(typeof(BattleKeepedCardDataInUnitModel), "AddBehaviours", new[] { typeof(LOR_DiceSystem.DiceCardXmlInfo), typeof(List<BattleDiceBehavior>) })]
    public class PrefixPatch_AddBehaviours1
    {
        public static void Prefix(List<BattleDiceBehavior> behaviourList)
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

    [HarmonyPatch(typeof(BattleKeepedCardDataInUnitModel), "AddBehaviours", new[] { typeof(BattleDiceCardModel), typeof(List<BattleDiceBehavior>) })]
    public class PrefixPatch_AddBehaviours2
    {
        public static void Prefix(List<BattleDiceBehavior> behaviourList)
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

    [HarmonyPatch(typeof(BattleKeepedCardDataInUnitModel), "AddBehaviour", new[] { typeof(BattleDiceCardModel), typeof(BattleDiceBehavior) })]
    public class PrefixPatch_AddBehaviour
    {
        public static void Prefix(BattleDiceBehavior behaviour)
        {
            if (behaviour.card.card != null && behaviour.card.card.HasBuf<PassiveAbility_Prescript.BattleDiceCardBuf_IndexMark>())
            {
                behaviour.abilityList.Add(new PassiveAbility_Prescript.DiceCardAbility_Marker());
            }
        }
    }

    [HarmonyPatch(typeof(BattleKeepedCardDataInUnitModel), "AddBehaviourForOnlyDefense", new[] { typeof(BattleDiceCardModel), typeof(BattleDiceBehavior) })]
    public class PrefixPatch_AddBehaviourForOnlyDefs
    {
        public static void Prefix(BattleDiceBehavior behaviour)
        {
            if (behaviour.card.card != null && behaviour.card.card.HasBuf<PassiveAbility_Prescript.BattleDiceCardBuf_IndexMark>())
            {
                behaviour.abilityList.Add(new PassiveAbility_Prescript.DiceCardAbility_Marker());
            }
        }
    }
}
