global using DeviceOfHermes.AdvancedBase;
global using DeviceOfHermes;

using HarmonyLib;
using LOR_XML;
using DeviceOfHermes.CustomDice;
using DeviceOfHermes.Resource;

public class SuperKurokumo : ModInitializer
{
    public static string packageId => "SuperKurokumo";

    public override void OnInitializeMod()
    {
        ApplyHarmonyPatch();

        var _ = new UnbreakableDice();

        var root = typeof(SuperKurokumo).GetAsmDirectory();

        Artwork.LoadBattleUnitBufSprites(Path.Combine(root, "Artwork", "BattleUnitBuf"));

        TextModel.OnLoadLocalize += lang =>
        {
            var path = Path.Combine(root, "Localize", "jp");

            var effectsPath = Path.Combine(path, "BattleEffectTexts", "EffectTexts.xml");
            var effectTexts = ReadXmlParser.Read<BattleEffectTextRoot>(effectsPath);

            effectTexts?.Let(txts => TextModel.SetBattleEffectTexts(txts.effectTextList, true));
        };
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
