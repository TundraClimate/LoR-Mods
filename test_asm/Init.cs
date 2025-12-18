using System;
using HarmonyLib;

namespace TestMod
{
    public class Test : ModInitializer
    {
        public static string PackageId
        {
            get
            {
                return "TestMod";
            }
        }

        public override void OnInitializeMod()
        {
            Harmony harmony = new Harmony(Test.PackageId);
            foreach (Type type in typeof(PatchClass).GetNestedTypes(AccessTools.all))
            {
                harmony.CreateClassProcessor(type).Patch();
            }
        }
    }
}
