using HarmonyLib;

public class ZikansaOffice : ModInitializer
{
    public static string packageId
    {
        get
        {
            return "ZikansaOffice";
        }
    }

    public override void OnInitializeMod()
    {
        ApplyHarmonyPatch();
    }

    private static void ApplyHarmonyPatch()
    {
        Harmony harmony = new Harmony(ZikansaOffice.packageId);
        foreach (Type type in typeof(PatchClass).GetNestedTypes(AccessTools.all))
        {
            harmony.CreateClassProcessor(type).Patch();
        }
    }
}
