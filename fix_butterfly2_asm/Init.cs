using System;
using HarmonyLib;

namespace Butterfly2FixMod
{
    public class Butterfly2Fix : ModInitializer
    {
        public static string PackageId 
        {
            get 
            {
                return "Butterfly2FixMod";
            }
        }

        public override void OnInitializeMod()
        {
            Harmony harmony = new Harmony(Butterfly2Fix.PackageId);
            foreach (Type type in typeof(PatchClass).GetNestedTypes(AccessTools.all))
            {
                harmony.CreateClassProcessor(type).Patch();
            }
        }
    }
}
