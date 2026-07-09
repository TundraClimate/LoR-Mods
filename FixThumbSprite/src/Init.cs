using HarmonyLib;

public class FixThumbSprite : ModInitializer
{
    public static string packageId
    {
        get
        {
            return "FixThumbSprite";
        }
    }

    public override void OnInitializeMod()
    {
        ApplyHarmonyPatch();
    }

    private static void ApplyHarmonyPatch()
    {
        Harmony harmony = new Harmony(FixThumbSprite.packageId);
        foreach (Type type in typeof(PatchClass).GetNestedTypes(AccessTools.all))
        {
            harmony.CreateClassProcessor(type).Patch();
        }
    }
}
