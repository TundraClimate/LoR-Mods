using System;
using HarmonyLib;

public class PowerfulEstherMOD : ModInitializer
{
    public static string packageId
    {
        get
        {
            return "PowerfulEstherMOD";
        }
    }

    public override void OnInitializeMod()
    {
        PowerfulEstherMOD.ApplyHarmonyPatch();
        ModResource.LoadAdditionals();
    }

    private static void ApplyHarmonyPatch()
    {
        Harmony harmony = new Harmony(PowerfulEstherMOD.packageId);
        foreach (Type type in typeof(PatchClass).GetNestedTypes(AccessTools.all))
        {
            harmony.CreateClassProcessor(type).Patch();
        }
    }
}
