using HarmonyLib;

public static class PatchClass
{
    [HarmonyPatch(typeof(BookModel), "CanSuccessionPassive")]
    class TempPatch
    {
        static void Postfix(BookModel __instance, ref bool __result, out GivePassiveState haspassiveState)
        {
            haspassiveState = GivePassiveState.Lock;
            __result = false;
        }
    }
}
