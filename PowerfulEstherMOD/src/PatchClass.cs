using System.Collections.Generic;
using LOR_DiceSystem;
using HarmonyLib;

public static class PatchClass
{
    [HarmonyPatch(typeof(BookModel), "SetXmlInfo")]
    public class PostfixPatch_SetXmlInfo
    {
        public static void Postfix(BookXmlInfo classInfo, List<DiceCardXmlInfo> ____onlyCards)
        {
            if (classInfo.id.packageId == PowerfulEstherMOD.packageId)
            {
                ____onlyCards.Clear();

                foreach (int id in classInfo.EquipEffect.OnlyCard)
                {
                    LorId lid = new LorId(PowerfulEstherMOD.packageId, id);

                    DiceCardXmlInfo info = ItemXmlDataList.instance.GetCardItem(lid);

                    if (info != null)
                    {
                        ____onlyCards.Add(info);
                    }
                }
            }
        }
    }

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
                if (beh.card == null)
                {
                    continue;
                }

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
                if (beh.card == null)
                {
                    continue;
                }

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
            if (behaviour.card == null)
            {
                return;
            }

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
            if (behaviour.card == null)
            {
                return;
            }

            if (behaviour.card.card != null && behaviour.card.card.HasBuf<PassiveAbility_Prescript.BattleDiceCardBuf_IndexMark>())
            {
                behaviour.abilityList.Add(new PassiveAbility_Prescript.DiceCardAbility_Marker());
            }
        }
    }

    [HarmonyPatch(typeof(StageController), "SetCurrentDiceActionPhase")]
    public class Patch_Postfix_SetCurrentDiceActionPhase
    {
        public static void Postfix(ref List<BattlePlayingCardDataInUnitModel> ____allCardList, ref StageController.StagePhase ____phase)
        {
            if (____phase != StageController.StagePhase.RoundEndPhase)
            {
                return;
            }

            BattleUnitModel target = BattleObjectManager.instance.GetAliveList(Faction.Enemy).Find(unit => unit.bufListDetail.HasBuf<BattleUnitBuf_TargetOfPrescript>());
            BattleUnitModel esther = BattleObjectManager.instance.GetAliveList(Faction.Player).Find(unit => unit.passiveDetail.HasPassive<PassiveAbility_Prescript>());

            if (target == null || esther == null)
            {
                return;
            }

            BattleKeepedCardDataInUnitModel keepCard = esther.cardSlotDetail.keepCard;

            if (keepCard == null)
            {
                return;
            }

            List<BattleDiceBehavior> keepCardDices = keepCard.GetDiceBehaviorList();
            List<BattleDiceBehavior> usingDices = new List<BattleDiceBehavior>();

            foreach (BattleDiceBehavior dice in keepCardDices)
            {
                if (dice.abilityList.Exists(abi => abi is DiceCardAbility_TheHealBreakLifeD3) || dice.abilityList.Exists(abi => abi is DiceCardAbility_TheHealBreakLifeD4))
                {
                    usingDices.Add(dice);
                }
            }

            keepCard.RemoveAllDice();

            if (usingDices.Count == 0)
            {
                return;
            }

            BattleDiceCardModel card = BattleDiceCardModel.CreatePlayingCard(ItemXmlDataList.instance.GetCardItem(new LorId(PowerfulEstherMOD.packageId, 29)));

            BattlePlayingCardDataInUnitModel playcard = new BattlePlayingCardDataInUnitModel()
            {
                owner = esther,
                card = card,
                target = target,
                targetSlotOrder = 0,
            };

            playcard.RemoveAllDice();

            foreach (BattleDiceBehavior dice in usingDices)
            {
                BattleDiceBehavior newDice = new BattleDiceBehavior();
                DiceBehaviour beh = dice.behaviourInCard.Copy();

                beh.Type = BehaviourType.Atk;

                newDice.behaviourInCard = beh;
                newDice.card = playcard;
                newDice.card.card = card;

                playcard.cardBehaviorQueue.Enqueue(newDice);
            }

            ____allCardList.Add(playcard);

            ____phase = StageController.StagePhase.SetCurrentDiceAction;
        }
    }
}
