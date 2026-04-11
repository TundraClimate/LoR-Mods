global using DeviceOfHermes.AdvancedBase;
using HarmonyLib;

public class TundraPassivePack : ModInitializer
{
    public static string packageId => "TundraPassivePack";

    public override void OnInitializeMod()
    {
        ApplyHarmonyPatch();
    }

    private static void ApplyHarmonyPatch()
    {
        Harmony harmony = new Harmony(TundraPassivePack.packageId);
        foreach (Type type in typeof(PatchClass).GetNestedTypes(AccessTools.all))
        {
            harmony.CreateClassProcessor(type).Patch();
        }
    }
}
