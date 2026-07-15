using HarmonyLib;

public class DeathThumb : ModInitializer
{
    public static string packageId => "DeathThumb";

    public override void OnInitializeMod()
    {
        ApplyHarmonyPatch();
    }

    private static void ApplyHarmonyPatch()
    {
        var harmony = new Harmony(DeathThumb.packageId);

        foreach (var type in typeof(PatchClass).GetNestedTypes(AccessTools.all))
        {
            harmony.CreateClassProcessor(type).Patch();
        }
    }
}
