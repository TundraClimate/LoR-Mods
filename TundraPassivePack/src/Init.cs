global using DeviceOfHermes;
global using DeviceOfHermes.AdvancedBase;
using HarmonyLib;
using DeviceOfHermes.Resource;

public class TundraPassivePack : ModInitializer
{
    public static string packageId => "TundraPassivePack";

    public override void OnInitializeMod()
    {
        Artwork.LoadBattleUnitBufSprites(Path.Combine(typeof(TundraPassivePack).GetAsmDirectory(), "Artwork", "BattleUnitBuf"));

        ApplyHarmonyPatch();

        VannilaUnitBuf.AddMaxIf<BattleUnitBuf_warpCharge>(80, (_, owner) => owner?.passiveDetail?.HasPassive<PassiveAbility_TundraPassivePack_HeavyBattery>() == true);
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
