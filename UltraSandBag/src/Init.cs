global using DeviceOfHermes;
global using DeviceOfHermes.AdvancedBase;
using DeviceOfHermes.Resource;
using LOR_XML;
using HarmonyLib;

public class UltraSandBagMOD : ModInitializer
{
    public static string packageId => "UltraSandBagMOD";

    public override void OnInitializeMod()
    {
        ApplyHarmonyPatch();

        Artwork.LoadBattleUnitBufSprites(Path.Combine(typeof(UltraSandBagMOD).GetAsmDirectory(), "Artwork", "BattleUnitBuf"));
        TextModel.SetBattleEffectTexts(Serde.FromXmlFile<BattleEffectTextRoot>(Path.Combine(typeof(UltraSandBagMOD).GetAsmDirectory(), "Localize", "jp", "BattleEffectTexts", "EffectTexts.xml"))!.effectTextList);
    }

    private static void ApplyHarmonyPatch()
    {
        Harmony harmony = new Harmony(UltraSandBagMOD.packageId);
        foreach (Type type in typeof(PatchClass).GetNestedTypes(AccessTools.all))
        {
            harmony.CreateClassProcessor(type).Patch();
        }
    }
}
