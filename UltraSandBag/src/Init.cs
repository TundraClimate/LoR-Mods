using System;
using HarmonyLib;

public class UltraSandBagMOD : ModInitializer
{
    public static string packageId
    {
        get
        {
            return "UltraSandBagMOD";
        }
    }

    public override void OnInitializeMod()
    {
        UltraSandBagMOD.ApplyHarmonyPatch();
        ModResource.LoadAdditionals();
    }

    private static void ApplyHarmonyPatch()
    {
        Harmony harmony = new Harmony(UltraSandBagMOD.packageId);
        foreach (Type type in typeof(PatchClass).GetNestedTypes(AccessTools.all))
        {
            harmony.CreateClassProcessor(type).Patch();
        }
    }
}
