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
        private LocalizeXml(string packageId)
        {
            this._localizeHarmony = new Harmony(packageId + ".Localize");
        }

        public static LocalizeXml<T> Init(string defaultLang)
        {
            _packageId = ModPackage<T>.PackageId;
            _localizePath = ModPackage<T>.AssemblyPath + "\\Localize\\";
            _defaultLang = defaultLang.ToLower();

            return new LocalizeXml<T>(_packageId);
        }

        public void ApplyBattleEffectTextsPatch()
        {
            this._localizeHarmony.CreateClassProcessor(typeof(LocalizeBattleEffectTexts)).Patch();
        }

        public void ApplyBattleCardAbilityDescPatch()
        {
            this._localizeHarmony.CreateClassProcessor(typeof(LocalizeBattleCardAbilityDesc)).Patch();
        }

        public void ApplyBattleCardDescPatch()
        {
            this._localizeHarmony.CreateClassProcessor(typeof(LocalizeBattleCardDesc)).Patch();
        }

        public void ApplyBattleDialogPatch()
        {
            this._localizeHarmony.CreateClassProcessor(typeof(LocalizeBattleDialog)).Patch();
        }

        public void ApplyBookDescPatch()
        {
            this._localizeHarmony.CreateClassProcessor(typeof(LocalizeBookDesc)).Patch();
        }

        public void ApplyCharacterNamePatch()
        {
            this._localizeHarmony.CreateClassProcessor(typeof(LocalizeCharacterName)).Patch();
        }

        public void ReloadLocalize()
        {
            string lang = GlobalGameManager.Instance.CurrentOption.language;

            LocalizedTextLoader.Instance.LoadBattleEffectTexts(lang);
            LocalizedTextLoader.Instance.LoadBattleCardAbilityDescriptions(lang);
            LocalizedTextLoader.Instance.LoadBattleCardDescriptions(lang);
            LocalizedTextLoader.Instance.LoadBattleDialogues(lang);
            LocalizedTextLoader.Instance.LoadBookDescriptions(lang);
            LocalizedTextLoader.Instance.LoadCharactersName(lang);
        }

        private Harmony _localizeHarmony;

        private static string _packageId;

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
                MethodInfo target = AccessTools.Method(typeof(BattleCardAbilityDescXmlList), nameof(BattleCardAbilityDescXmlList.Init));
                MethodInfo inject = AccessTools.Method(typeof(LocalizeBattleCardAbilityDesc), nameof(LocalizeBattleCardAbilityDesc.LoadBattleCardAbilityDescXmls));

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

        [HarmonyPatch(typeof(LocalizedTextLoader), nameof(LocalizedTextLoader.LoadBattleCardDescriptions))]
        private class LocalizeBattleCardDesc
        {
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                MethodInfo target = AccessTools.Method(typeof(BattleCardDescXmlList), nameof(BattleCardDescXmlList.Init));
                MethodInfo inject = AccessTools.Method(typeof(LocalizeBattleCardDesc), nameof(LocalizeBattleCardDesc.LoadBattleCardDescXmls));

                return instructions.InjectBefore(target, inject);
            }

            static void LoadBattleCardDescXmls(Dictionary<LorId, BattleCardDesc> dictionary, string language)
            {
                string path = Path.Combine(_localizePath, language, "BattleCardDesc");

                if (!PatchUtil.TryExistsWithDefault(_defaultLang, ref path))
                {
                    return;
                }

                PatchUtil.EachXmlAt<BattleCardDescRoot>(path, xml =>
                {
                    var elements = xml.cardDescList;

                    foreach (var elem in elements)
                    {
                        dictionary.Add(new LorId(_packageId, elem.cardID), elem);
                    }
                });
            }
        }

        [HarmonyPatch(typeof(LocalizedTextLoader), nameof(LocalizedTextLoader.LoadBattleDialogues))]
        private class LocalizeBattleDialog
        {
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
            {
                MethodInfo target = AccessTools.Method(typeof(BattleDialogXmlList), nameof(BattleDialogXmlList.Init));
                MethodInfo inject = AccessTools.Method(typeof(LocalizeBattleDialog), nameof(LocalizeBattleDialog.LoadBattleDialogXmls));

                foreach (CodeInstruction inst in instructions)
                {
                    if (inst.Calls(target))
                    {
                        var stash1 = generator.DeclareLocal(typeof(Dictionary<string, BattleDialogRoot>));
                        var stash2 = generator.DeclareLocal(typeof(List<BattleDialogRelationWithBookID>));

                        yield return new CodeInstruction(OpCodes.Stloc, stash2);
                        yield return new CodeInstruction(OpCodes.Stloc, stash1);

                        yield return new CodeInstruction(OpCodes.Ldloc, stash1);
                        yield return new CodeInstruction(OpCodes.Ldloc, stash2);
                        yield return new CodeInstruction(OpCodes.Ldloc, stash1);

                        yield return new CodeInstruction(OpCodes.Ldarg_1);
                        yield return new CodeInstruction(OpCodes.Call, inject);
                    }

                    yield return inst;
                }
            }

            static void LoadBattleDialogXmls(Dictionary<string, BattleDialogRoot> dictionary, string language)
            {
                string path = Path.Combine(_localizePath, language, "BattleDialog");

                if (!PatchUtil.TryExistsWithDefault(_defaultLang, ref path))
                {
                    return;
                }

                PatchUtil.EachXmlAt<BattleDialogRoot>(path, xml =>
                {
                    dictionary.Add(xml.groupName, xml);
                });
            }
        }

        [HarmonyPatch(typeof(LocalizedTextLoader), nameof(LocalizedTextLoader.LoadBookDescriptions))]
        private class LocalizeBookDesc
        {
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                MethodInfo target = AccessTools.Method(typeof(BookDescXmlList), nameof(BookDescXmlList.Init));
                MethodInfo inject = AccessTools.Method(typeof(LocalizeBookDesc), nameof(LocalizeBookDesc.LoadBookXmlDesc));

                return instructions.InjectBefore(target, inject);
            }

            static void LoadBookXmlDesc(Dictionary<int, BookDesc> dictionary, string language)
            {
                string path = Path.Combine(_localizePath, language, "BookDesc");

                if (!PatchUtil.TryExistsWithDefault(_defaultLang, ref path))
                {
                    return;
                }

                PatchUtil.EachXmlAt<ModBookDescRoot>(path, xml =>
                {
                    var books = xml.bookDescList;
                    Dictionary<string, List<BookDesc>> workshopBooks = new Dictionary<string, List<BookDesc>>();

                    foreach (var book in books)
                    {
                        BookDesc vannilaBook = new BookDesc()
                        {
                            bookID = book.bookID,
                            bookName = book.bookName,
                            texts = book.texts,
                            passives = book.passives,
                        };

                        if (book.pid == "@origin")
                        {
                            dictionary.Add(book.bookID, vannilaBook);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(book.pid))
                            {
                                book.pid = _packageId;
                            }

                            if (!workshopBooks.TryGetValue(book.pid, out var descs))
                            {
                                descs = new List<BookDesc>();
                            }

                            descs.Add(vannilaBook);
                        }
                    }

                    foreach (var workshopBook in workshopBooks)
                    {
                        BookDescXmlList.Instance.AddBookTextByMod(workshopBook.Key, workshopBook.Value);
                    }
                });
            }
        }

        [HarmonyPatch(typeof(LocalizedTextLoader), nameof(LocalizedTextLoader.LoadCharactersName))]
        private class LocalizeCharacterName
        {
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
            {
                MethodInfo target = AccessTools.Method(typeof(CharactersNameXmlList), nameof(CharactersNameXmlList.Init));
                MethodInfo inject = AccessTools.Method(typeof(LocalizeCharacterName), nameof(LocalizeCharacterName.LoadCharacterNameXmls));

                return instructions.InjectBefore(target, inject);
            }

            static void LoadCharacterNameXmls(CharactersNameRoot nameRoot, string language)
            {
                string path = Path.Combine(_localizePath, language, "CharacterName");

                if (!PatchUtil.TryExistsWithDefault(_defaultLang, ref path))
                {
                    return;
                }

                PatchUtil.EachXmlAt<ModCharactersNameRoot>(path, xml =>
                {
                    var nameList = xml.nameList;

                    foreach (var name in nameList)
                    {
                        string pid = name.pid;

                        CharacterName vannilaName = new CharacterName()
                        {
                            ID = name.ID,
                            name = name.name,
                        };

                        if (pid == "@origin")
                        {
                            var locaName = nameRoot.nameList.Find(n => n.ID == name.ID);

                            if (locaName == null)
                            {
                                nameRoot.nameList.Add(vannilaName);
                            }
                            else
                            {
                                locaName.name = name.name;
                            }

                            continue;
                        }

                        if (string.IsNullOrEmpty(pid))
                        {
                            pid = _packageId;
                        }

                        var moddedUnit = EnemyUnitClassInfoList.Instance.GetDataFromWorkshop(pid, name.ID);

                        if (moddedUnit != null)
                        {
                            moddedUnit.name = name.name;
                        }
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
