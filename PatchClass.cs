using HarmonyLib;

namespace WarpClassFive
{
    public class PatchClass
    {
        [HarmonyPatch(typeof(BattleUnitBuf_warpCharge), "OnAddBuf")]
        public class PrefixPatch_SetMaxmimChargeCount
        {
            public static bool Prefix(BattleUnitBuf_warpCharge __instance, BattleUnitModel ____owner)
            {
                if (__instance.stack > 80)
                {
                    __instance.stack = 80;
                }
                if (____owner.IsImmune(__instance.bufType))
                {
                    __instance.stack = 0;
                }
                return false;
            }
        }
    }
}
