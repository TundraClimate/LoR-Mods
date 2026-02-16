using System;
using HarmonyLib;

public class ThumbAmmo : ModInitializer
{
    public static string packageId
    {
        get
        {
            return "ThumbAmmo";
        }
    }

    public override void OnInitializeMod()
    {
        ApplyHarmonyPatch();
        ModResource.LoadAdditionals();
    }

    private static void ApplyHarmonyPatch()
    {
        Harmony harmony = new Harmony(ThumbAmmo.packageId);
        foreach (Type type in typeof(PatchClass).GetNestedTypes(AccessTools.all))
        {
            harmony.CreateClassProcessor(type).Patch();
        }
    }
}
