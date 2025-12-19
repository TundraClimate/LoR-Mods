using System;
using HarmonyLib;

namespace CanChangeWasTargetMod
{
    public class CanChangeWasTarget : ModInitializer
    {
        public static string PackageId
        {
            get
            {
                return "CanChangeWasTargetMod";
            }
        }

        public override void OnInitializeMod()
        {
            Harmony harmony = new Harmony(CanChangeWasTarget.PackageId);
            foreach (Type type in typeof(PatchClass).GetNestedTypes(AccessTools.all))
            {
                harmony.CreateClassProcessor(type).Patch();
            }
        }
    }
}
