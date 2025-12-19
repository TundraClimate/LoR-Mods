using System;
using HarmonyLib;

namespace SmartFarAttackMod
{
    public class SmartFarAttack : ModInitializer
    {
        public static string PackageId
        {
            get
            {
                return "SmartFarAttackMod";
            }
        }

        public override void OnInitializeMod()
        {
            Harmony harmony = new Harmony(SmartFarAttack.PackageId);
            foreach (Type type in typeof(PatchClass).GetNestedTypes(AccessTools.all))
            {
                harmony.CreateClassProcessor(type).Patch();
            }
        }
    }
}
