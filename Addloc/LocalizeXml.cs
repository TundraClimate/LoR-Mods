using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using LOR_XML;

namespace Addloc
{
    public class LocalizeXml<T>
        where T : ModPackage<T>, new()
    {
        private LocalizeXml(string packageId, string packagePath)
        {
            this._packageId = packageId;
            this._localizeHarmony = new Harmony(packageId + ".Localize");
        }

        public static LocalizeXml<T> Init()
        {
            _localizePath = ModPackage<T>.AssemblyPath + "\\Localize\\";

            return new LocalizeXml<T>(ModPackage<T>.PackageId, ModPackage<T>.AssemblyPath);
        }

        public void ApplyBattleEffectTextsPatch()
        {
            this._localizeHarmony.CreateClassProcessor(typeof(LocalizeBattleEffectTexts)).Patch();

            string lang = GlobalGameManager.Instance.CurrentOption.language;

            LocalizedTextLoader.Instance.LoadBattleEffectTexts(lang);
        }

        private Harmony _localizeHarmony;

        private string _packageId;

        private static string _localizePath;

        [HarmonyPatch(typeof(LocalizedTextLoader), nameof(LocalizedTextLoader.LoadBattleEffectTexts))]
        private class LocalizeBattleEffectTexts
        {
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                MethodInfo target = AccessTools.Method(typeof(BattleEffectTextsXmlList), nameof(BattleEffectTextsXmlList.Init));
                MethodInfo inject = AccessTools.Method(typeof(LocalizeBattleEffectTexts), nameof(LocalizeBattleEffectTexts.LoadBattleEffectTextsXmls));

                foreach (CodeInstruction inst in instructions)
                {
                    if (inst.Calls(target))
                    {
                        yield return new CodeInstruction(OpCodes.Dup);
                        yield return new CodeInstruction(OpCodes.Ldarg_1);
                        yield return new CodeInstruction(OpCodes.Call, inject);
                    }

                    yield return inst;
                }
            }

            static void LoadBattleEffectTextsXmls(Dictionary<string, BattleEffectText> dictionary, string language)
            {
                string path = Path.Combine(_localizePath, language, "BattleEffectTexts");

                XmlSerializer serializer = new XmlSerializer(typeof(BattleEffectTextRoot));

                foreach (string effectTexts in Walkdir.GetFilesRecursive(path))
                {
                    if (!effectTexts.EndsWith(".xml"))
                    {
                        continue;
                    }

                    using (StreamReader reader = new StreamReader(effectTexts))
                    {
                        List<BattleEffectText> texts = ((BattleEffectTextRoot)serializer.Deserialize(reader)).effectTextList;

                        foreach (BattleEffectText text in texts)
                        {
                            dictionary.Add(text.ID, text);
                        }
                    }
                }
            }
        }
    }
}
