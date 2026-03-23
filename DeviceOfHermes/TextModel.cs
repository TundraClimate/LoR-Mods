using HarmonyLib;
using LOR_XML;

namespace DeviceOfHermes.Resource;

public static class TextModel
{
    static TextModel()
    {
        _effTxtRef = AccessTools.FieldRefAccess<BattleEffectTextsXmlList, Dictionary<string, BattleEffectText>>("_dictionary");
    }

    public static void SetBattleEffectText(BattleEffectText text, bool replace = false)
    {
        ref var dict = ref EffectTextDict;

        if (dict.ContainsKey(text.ID))
        {
            if (replace)
            {
                dict[text.ID] = text;
            }
            else
            {
                Hermes.Say($"Skipped: BattleEffectText the '{text.ID}' is already exists.", MessageLevel.Warn);
            }

            return;
        }

        dict.Add(text.ID, text);
    }

    private static ref Dictionary<string, BattleEffectText> EffectTextDict => ref _effTxtRef(BattleEffectTextsXmlList.Instance);

    private static readonly AccessTools.FieldRef<BattleEffectTextsXmlList, Dictionary<string, BattleEffectText>> _effTxtRef;
}
