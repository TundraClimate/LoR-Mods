global using DeviceOfHermes;
global using DeviceOfHermes.AdvancedBase;
using DeviceOfHermes.Resource;
using LOR_XML;

public class UltraSandBagMOD : ModInitializer
{
    public static string packageId => "UltraSandBagMOD";

    public override void OnInitializeMod()
    {
        Artwork.LoadBattleUnitBufSprites(Path.Combine(typeof(UltraSandBagMOD).GetAsmDirectory(), "Artwork", "BattleUnitBuf"));
        TextModel.SetBattleEffectTexts(Serde.FromXmlFile<BattleEffectTextRoot>(Path.Combine(typeof(UltraSandBagMOD).GetAsmDirectory(), "Localize", "jp", "BattleEffectTexts", "EffectTexts.xml"))!.effectTextList);
    }
}
