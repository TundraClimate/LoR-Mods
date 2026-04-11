using HarmonyLib;
using UnityEngine;

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

    [HarmonyPatch(typeof(BookInventoryModel), "LoadFromSaveData")]
    class PatchOn
    {
        static void Postfix()
        {
            Hermes.Say("Load corepages");

            var id = new LorId(TestMOD.packageId, 10000001);

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

    [HarmonyPatch(typeof(UI.UIStoryProgressPanel), "SetStoryLine")]
    class PatchTemp
    {
        static void Postfix()
        {
            var center = (UI.UIController.Instance?.GetUIPanel(UI.UIPanelType.Invitation) as UI.UIInvitationPanel)?
                .InvCenterStoryPanel;

            if (center is null)
            {
                Hermes.Say("center is null");
                return;
            }

            var iconList = (List<UI.UIStoryProgressIconSlot>)center.GetType().Field("iconList").GetValue(center);

            if (iconList is null)
            {
                Hermes.Say("iconList is null");
                return;
            }

            foreach (var icon in iconList)
            {
                if (icon is null)
                {
                    continue;
                }

                if (icon.currentStory == UI.UIStoryLine.YunsOffice)
                {
                    var conLines = (List<GameObject>)icon.GetType().Field("connectLineList").GetValue(icon);

                    if (conLines.Count == 0)
                    {
                        continue;
                    }

                    var lineBase = conLines[0];

                    var ofsy = 250;

                    (float, float) posA = (-1400f, 5420f + ofsy);
                    (float, float) posB = (1460f, 6000f + ofsy);

                    var createAt = ((posA.Item1 + posB.Item1) / 2, (posA.Item2 + posB.Item2) / 2);
                    var dx = posB.Item1 - posA.Item1;
                    var dy = posB.Item2 - posA.Item2;
                    var rotate = Math.Atan2(dy, dx) * 180f / Math.PI;
                    var scale = (Math.Sqrt(dx * dx + dy * dy) / 150f);

                    var newLine = UnityEngine.Object.Instantiate(lineBase, lineBase.transform.parent);

                    newLine.transform.localPosition = new Vector3(createAt.Item1, createAt.Item2);
                    newLine.transform.localRotation = Quaternion.Euler(1f, 1f, ((float)rotate));
                    newLine.transform.localScale = new Vector3(((float)scale), 1f, 1f);

                    conLines.Add(newLine);
                }
            }
        }
    }
}
