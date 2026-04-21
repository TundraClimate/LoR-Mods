using System.Reflection.Emit;
using HarmonyLib;

public static class PatchClass
{
    [HarmonyPatch(typeof(BattleCamManager), "UpdateManual")]
    class PatchZoom
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var matcher = new CodeMatcher(instructions);

            var target = AccessTools.Method(typeof(UnityEngine.Mathf), "Clamp", [typeof(float), typeof(float), typeof(float)]);

            matcher.MatchStartForward(new CodeMatch(i => i.Calls(target)))
                .SetInstruction(new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(PatchZoom), "InjectMethod")));

            return matcher.Instructions();
        }

        static float InjectMethod(float num, float min, float max)
        {
            return num;
        }
    }
}
