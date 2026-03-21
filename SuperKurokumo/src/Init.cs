global using DeviceOfHermes.AdvancedBase;
global using DeviceOfHermes;

using HarmonyLib;
using Addloc;

public class SuperKurokumo : ModPackage<SuperKurokumo>
{
    public override string packageId => "SuperKurokumo";

    public override void OnInitializeMod()
    {
        ApplyHarmonyPatch();

        ModResource.RegisterMOD<SuperKurokumo>();
    }

    private static void ApplyHarmonyPatch()
    {
        Harmony harmony = new Harmony(SuperKurokumo.PackageId);
        foreach (Type type in typeof(PatchClass).GetNestedTypes(AccessTools.all))
        {
            harmony.CreateClassProcessor(type).Patch();
        }
    }
}
