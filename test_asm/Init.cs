using System;
using HarmonyLib;

public class TestMod : ModInitializer
{
    public static string packageId
    {
        get
        {
            return "TestMod";
        }
    }

    public override void OnInitializeMod()
    {
        TestMod.ApplyHarmonyPatches();
        ModResource.RegisterResources();
    }

    private static void ApplyHarmonyPatches()
    {
        Harmony harmony = new Harmony(TestMod.packageId);
        foreach (Type type in typeof(PatchClass).GetNestedTypes(AccessTools.all))
        {
            harmony.CreateClassProcessor(type).Patch();
        }
    }
}
