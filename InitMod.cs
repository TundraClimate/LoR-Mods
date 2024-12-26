using System;
using HarmonyLib;

namespace WarpClassFive
{
    public class WarpClassFiveMod : ModInitializer
    {
        public static string packageId
        {
            get
            {
                return "WarpClassFive";
            }
        }

        public override void OnInitializeMod()
        {
            Harmony harmony = new Harmony(WarpClassFiveMod.packageId);
            foreach (Type type in typeof(PatchClass).GetNestedTypes(AccessTools.all))
            {
                harmony.CreateClassProcessor(type).Patch();
            }
        }
    }
}
