using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using HarmonyExtension;
using UnityEngine;
using DeviceOfHermes;

public static class PatchClass
{
    [HarmonyPatch(typeof(UI.UIColorManager), "get_DiceColors")]
    class PatchDiceColors
    {
        static void Postfix(ref Color[] __result, Color[] ____diceColors)
        {
            __result = ____diceColors;
        }
    }

    [HarmonyPatch(typeof(UI.UIColorManager), "get_DiceLinearDodgeColors")]
    class PatchLidColors
    {
        static void Postfix(ref Color[] __result, Color[] ____diceLinearDodgeColors)
        {
            __result = ____diceLinearDodgeColors;
        }
    }

    [HarmonyPatch(typeof(UI.UIColorManager), "get_BehaviourColors")]
    class PatchBehColors
    {
        static void Postfix(ref Color[] __result, Color[] ____behaviourColors)
        {
            __result = ____behaviourColors;
        }
    }

    [HarmonyPatch(typeof(UI.UISpriteDataManager), "get__cardBehaviourDetailIcons")]
    class PatchDiceIcon
    {
        static void Postfix(ref List<Sprite> __result, List<Sprite> ___cardBehaviourDetailIcons)
        {
        }
    }

    [HarmonyPatch(typeof(BookModel), "get_ClassInfo")]
    class PatchSuccession
    {
        static void Postfix(ref BookXmlInfo __result)
        {
            __result.SuccessionPossibleNumber = 18;
        }
    }

    [HarmonyPatch(typeof(BookModel), "IsNotFullEquipPassiveBook")]
    class PatchEquip
    {
        static void Postfix(BookModel __instance, ref bool __result)
        {
            if (__instance.reservedData.equipedBookIdListInPassive.Count < 8)
            {
                __result = true;
            }
        }
    }

    [HarmonyPatch(typeof(BattleFarAreaPlayManager), "StartFarAreaPlay")]
    class PatchStartFarAreaPlay
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var matcher = new CodeMatcher(instructions);

            matcher.MatchStartForward(CodeMatch.Calls(typeof(List<BattlePlayingCardDataInUnitModel>).Method("get_Item")))
                .SetInstruction(CodeInstruction.Call(typeof(PatchStartFarAreaPlay).Method("InjectMethod")));

            matcher.MatchStartForward(CodeMatch.Calls(typeof(List<BattlePlayingCardDataInUnitModel>).Method("get_Item")))
                .SetInstruction(CodeInstruction.Call(typeof(PatchStartFarAreaPlay).Method("InjectMethod")));

            return matcher.Instructions();
        }

        static BattlePlayingCardDataInUnitModel InjectMethod(List<BattlePlayingCardDataInUnitModel> list, int index)
        {
            return list?.Count > index && index >= 0 ? list?[index] : null;
        }
    }

    [HarmonyPatch(typeof(BattleCamManager), "UpdateManual")]
    class PatchMouseCam
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var matcher = new CodeMatcher(instructions);

            matcher.End()
                .MatchEndBackwards(CodeMatch.Calls(typeof(BattleCamManager).Method("CheckBoundary")))
                .SetInstruction(new CodeInstruction(OpCodes.Call, typeof(PatchMouseCam).Method("InjectMethod")));

            matcher.MatchEndBackwards(CodeMatch.IsLdfld(), CodeMatch.Calls(typeof(Vector3).Method("op_Multiply", [typeof(Vector3), typeof(float)])))
                .Advance(1)
                .Insert(CodeInstruction.Literal<float>(10f), CodeInstruction.Call(typeof(Vector3).Method("op_Multiply", [typeof(Vector3), typeof(float)])));

            return matcher.Instructions();
        }

        static Vector3 InjectMethod(BattleCamManager __instance, Vector3 pos)
        {
            return pos;
        }
    }

    [HarmonyPatch]
    class PatchDelegate
    {
        static MethodBase TargetMethod()
        {
            return typeof(UI.UIInvenCardListScroll)
                .GetNestedTypes(AccessTools.all)
                .First(t => t.Name.Contains("DisplayClass49_0"))
                .Method("<ApplyFilterAll>b__7");
        }

        static bool Prefix(ref bool __result, DiceCardItemModel x)
        {
            if (x.GetID() == 608008)
            {
                __result = true;

                return false;
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(UI.UIInvenCardSlot), "SetSlotState")]
    class PatchLockState
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var matcher = new CodeMatcher(instructions);

            matcher.MatchStartForward(
                CodeMatch.IsLdarg(),
                new CodeMatch(i => i.opcode == OpCodes.Ldfld && i.OperandIs(AccessTools.Field(typeof(UI.UIInvenCardSlot), "deckLimitRoot")))
            )
                .Insert(
                    CodeInstruction.Nop.MoveLabelsFrom(matcher.Instruction),
                    CodeInstruction.Instance,
                    CodeInstruction.Instance,
                    CodeInstruction.Field(typeof(UI.UIInvenCardSlot).Field("_cardModel")),
                    CodeInstruction.Instance,
                    CodeInstruction.Field(typeof(UI.UIInvenCardSlot).Field("slotState")),
                    CodeInstruction.Call(typeof(PatchLockState).Method("InjectMethod")),
                    CodeInstruction.SetField(typeof(UI.UIInvenCardSlot).Field("slotState"))
                );

            return matcher.Instructions();
        }

        static UIINVENCARD_STATE InjectMethod(DiceCardItemModel cardModel, UIINVENCARD_STATE state)
        {
            if (cardModel.GetID() == 608008)
            {
                return UIINVENCARD_STATE.None;
            }

            return state;
        }
    }

    [HarmonyPatch(typeof(BookModel), "AddCardFromInventoryToCurrentDeck")]
    class PatchOnAddDeck
    {
        static void Postfix(DeckModel ____deck, ref CardEquipState __result, LorId cardId)
        {
            if (__result is CardEquipState.FarTypeLimit && cardId == 608008)
            {
                var card = ItemXmlDataList.instance.GetCardItem(cardId);

                if (InventoryModel.Instance.RemoveCard(card.id))
                {
                    ____deck.GetCardList_nocopy().Add(card);
                    __result = CardEquipState.Equippable;
                }
            }
        }
    }

    [HarmonyPatch(typeof(BookInventoryModel), "LoadFromSaveData")]
    class PatchOn
    {
        static void Postfix()
        {
            Hermes.Say("Load corepages");

            var id = new LorId(PackageInfo<TestMOD>.Id, 10000001);

            if (BookInventoryModel.Instance.GetBookCount(id) == 0)
            {
                var bm = BookInventoryModel.Instance.CreateBook(id);

                if (bm is not null)
                {
                    Hermes.Say(bm.GetName());
                }
            }
            else
            {
                Hermes.Say($"Already added the {id}");
            }
        }
    }
}
