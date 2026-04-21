using HarmonyLib;

public class MoreMoreZoom : ModInitializer
{
    public static string packageId
    {
        get
        {
            return "MoreMoreZoom";
        }
    }

    public override void OnInitializeMod()
    {
        ApplyHarmonyPatch();
    }

    private static void ApplyHarmonyPatch()
    {
        Harmony harmony = new Harmony(MoreMoreZoom.packageId);
        foreach (Type type in typeof(PatchClass).GetNestedTypes(AccessTools.all))
        {
            harmony.CreateClassProcessor(type).Patch();
        }
    }
}
