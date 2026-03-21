using HarmonyLib;

public class SuperKurokumo : ModInitializer
{
    public static string packageId
    {
        get
        {
            return "SuperKurokumo";
        }
    }

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
