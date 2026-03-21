global using DeviceOfHermes.AdvancedBase;
global using DeviceOfHermes;

using HarmonyLib;

public class SuperKurokumo : ModInitializer
{
    public static string packageId => "SuperKurokumo";

    public override void OnInitializeMod()
    {
        ApplyHarmonyPatch();
    }

    private static void ApplyHarmonyPatch()
    {
        Harmony harmony = new Harmony(SuperKurokumo.packageId);
        foreach (Type type in typeof(PatchClass).GetNestedTypes(AccessTools.all))
        {
            harmony.CreateClassProcessor(type).Patch();
        }
    }
}
