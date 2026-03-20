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
}
