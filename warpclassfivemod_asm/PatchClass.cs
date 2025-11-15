using HarmonyLib;
using WarpClassFive_Passive;

namespace WarpClassFive
{
    public class PatchClass
    {
        [HarmonyPatch(typeof(BattleUnitBuf_warpCharge), "OnAddBuf")]
        public class PrefixPatch_SetMaximumChargeCount
        {
            public static bool Prefix(BattleUnitBuf_warpCharge __instance, BattleUnitModel ____owner)
            {
                if (____owner != null && ____owner.passiveDetail.HasPassive<PassiveAbility_MoreCharge>())
                {
                    if (__instance.stack > 40)
                    {
                        __instance.stack = 40;
                    }
                    if (____owner.IsImmune(__instance.bufType))
                    {
                        __instance.stack = 0;
                    }
                    return false;
                }
                return true;
            }
        }
    }
}
