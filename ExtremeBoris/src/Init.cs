global using DeviceOfHermes;
global using DeviceOfHermes.AdvancedBase;
global using DeviceOfHermes.CustomDice;
global using LimbufOfHermes;
using DeviceOfHermes.Resource;
using LOR_XML;

public class ExtremeBoris : ModInitializer
{
    public static string packageId => "ExtremeBoris";

    public override void OnInitializeMod()
    {
        TextModel.OnLoadLocalize += OnLocalize;
    }

    static void OnLocalize(string lang)
    {
        var localize = Path.Combine(typeof(ExtremeBoris).GetAsmDirectory(), "Localize", lang);

        TextModel.SetBattleCardAbilityDescs(Serde.FromXmlFile<BattleCardAbilityDescRoot>(Path.Combine(localize, "CardAbility.xml"))!.cardDescList);
        TextModel.SetBattleCardDescs(Serde.FromXmlFile<BattleCardDescRoot>(Path.Combine(localize, "CardDesc.xml"))!.cardDescList.Map(desc => (packageId, desc))!);
        TextModel.SetBattleEffectTexts(Serde.FromXmlFile<BattleEffectTextRoot>(Path.Combine(localize, "EffectText.xml"))!.effectTextList);
    }
}
