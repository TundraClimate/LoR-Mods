using System;
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

        public static LocalizeXml<T> Init(string defaultLang)
        {
            _localizePath = ModPackage<T>.AssemblyPath + "\\Localize\\";
            _defaultLang = defaultLang.ToLower();

            return new LocalizeXml<T>(ModPackage<T>.PackageId, ModPackage<T>.AssemblyPath);
        }

        public void ApplyBattleEffectTextsPatch()
        {
            this._localizeHarmony.CreateClassProcessor(typeof(LocalizeBattleEffectTexts)).Patch();
        }

        public void ApplyBattleCardAbilityDescPatch()
        {
            this._localizeHarmony.CreateClassProcessor(typeof(LocalizeBattleCardAbilityDesc)).Patch();
        }

        public void ReloadLocalize()
        {
            string lang = GlobalGameManager.Instance.CurrentOption.language;

            LocalizedTextLoader.Instance.LoadBattleEffectTexts(lang);
            LocalizedTextLoader.Instance.LoadBattleCardAbilityDescriptions(lang);
        }

        private Harmony _localizeHarmony;

        private string _packageId;

        private static string _localizePath;

        private static string _defaultLang;

        [HarmonyPatch(typeof(LocalizedTextLoader), nameof(LocalizedTextLoader.LoadBattleEffectTexts))]
        private class LocalizeBattleEffectTexts
        {
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                MethodInfo target = AccessTools.Method(typeof(BattleEffectTextsXmlList), nameof(BattleEffectTextsXmlList.Init));
                MethodInfo inject = AccessTools.Method(typeof(LocalizeBattleEffectTexts), nameof(LocalizeBattleEffectTexts.LoadBattleEffectTextsXmls));

                return instructions.InjectBefore(target, inject);
            }

            static void LoadBattleEffectTextsXmls(Dictionary<string, BattleEffectText> dictionary, string language)
            {
                string path = Path.Combine(_localizePath, language, "BattleEffectTexts");

                if (!PatchUtil.TryExistsWithDefault(_defaultLang, ref path))
                {
                    return;
                }

                PatchUtil.EachXmlAt<BattleEffectTextRoot>(path, xml =>
                {
                    var elements = xml.effectTextList;

                    foreach (var elem in elements)
                    {
                        dictionary.Add(elem.ID, elem);
                    }
                });
            }
        }

        [HarmonyPatch(typeof(LocalizedTextLoader), nameof(LocalizedTextLoader.LoadBattleCardAbilityDescriptions))]
        private class LocalizeBattleCardAbilityDesc
        {
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                MethodInfo target = AccessTools.Method(typeof(BattleEffectTextsXmlList), nameof(BattleCardAbilityDescXmlList.Init));
                MethodInfo inject = AccessTools.Method(typeof(LocalizeBattleEffectTexts), nameof(LocalizeBattleCardAbilityDesc.LoadBattleCardAbilityDescXmls));

                return instructions.InjectBefore(target, inject);
            }

            static void LoadBattleCardAbilityDescXmls(Dictionary<string, BattleCardAbilityDesc> dictionary, string language)
            {
                string path = Path.Combine(_localizePath, language, "BattleCardAbilityDesc");

                if (!PatchUtil.TryExistsWithDefault(_defaultLang, ref path))
                {
                    return;
                }

                PatchUtil.EachXmlAt<BattleCardAbilityDescRoot>(path, xml =>
                {
                    var elements = xml.cardDescList;

                    foreach (var elem in elements)
                    {
                        dictionary.Add(elem.id, elem);
                    }
                });
            }
        }
    }

    internal static class PatchUtil
    {
        internal static IEnumerable<CodeInstruction> InjectBefore(this IEnumerable<CodeInstruction> instructions, MethodInfo target, MethodInfo inject)
        {
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

        internal static bool TryExistsWithDefault(string defaultLang, ref string path)
        {
            if (!Directory.Exists(path))
            {
                string locaType = Path.GetFileName(path);
                string localizeDir = Path.GetDirectoryName(Path.GetDirectoryName(path));

                path = Path.Combine(localizeDir, defaultLang, locaType);
            }

            return Directory.Exists(path);
        }

        internal static void EachXmlAt<T>(string atPath, Action<T> each)
        {
            var serializer = new XmlSerializer(typeof(T));

            foreach (string filePath in Walkdir.GetFilesRecursive(atPath))
            {
                if (!filePath.EndsWith(".xml"))
                {
                    continue;
                }

                using (StreamReader reader = new StreamReader(filePath))
                {
                    var xml = (T)serializer.Deserialize(reader);

                    each(xml);
                }
            }
        }
    }
}
