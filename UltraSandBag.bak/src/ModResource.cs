using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using HarmonyLib;
using LOR_XML;

public class ModResource
{
    public static void LoadAdditionals()
    {
        ModResource.LoadAdditionalArtworks();
        ModResource.LoadAdditionalXmls();
    }

    private static void LoadAdditionalArtworks()
    {
        string path = ModResource._asmPath + "\\Artwork\\";

        if (!Directory.Exists(path))
        {
            return;
        }

        ModResource.LoadBattleUnitBufArtworks(path);
    }

    private static void LoadBattleUnitBufArtworks(string artworkPath)
    {
        string battleUnitBufPath = artworkPath + "BattleUnitBuf";

        if (!Directory.Exists(artworkPath))
        {
            return;
        }

        Dictionary<string, Sprite> iconDict = BattleUnitBuf._bufIconDictionary;

        foreach (string unitBuf in Walkdir.GetFilesRecursive(battleUnitBufPath))
        {
            string fileName = Path.GetFileName(unitBuf);
            string unitBufId;

            if (fileName.EndsWith(".png"))
            {
                unitBufId = fileName.TrimEnd(".png".ToCharArray());
            }
            else if (fileName.EndsWith(".jpg"))
            {
                unitBufId = fileName.TrimEnd(".jpg".ToCharArray());
            }
            else
            {
                continue;
            }

            iconDict.Add(unitBufId, ModResource.CreateSprite(unitBuf));
        }
    }

    private static Sprite CreateSprite(string path)
    {
        byte[] pngData = File.ReadAllBytes(path);

        Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        if (!ImageConversion.LoadImage(texture, pngData))
        {
            return null;
        }

        return Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f),
            50f
        );
    }

    private static void LoadAdditionalXmls()
    {
        string path = ModResource._asmPath + "\\Localize\\";

        if (!Directory.Exists(path))
        {
            return;
        }

        Harmony harmony = new Harmony(UltraSandBagMOD.packageId + ".Resource");

        harmony.CreateClassProcessor(typeof(LocalizePatch_EffectTexts)).Patch();

        string lang = GlobalGameManager.Instance.CurrentOption.language;

        LocalizedTextLoader.Instance.LoadBattleEffectTexts(lang);
    }

    private static void LoadBattleEffectTextsXmls(Dictionary<string, BattleEffectText> dictionary, string language)
    {
        string path = string.Format("{0}\\Localize\\{1}\\{2}\\", ModResource._asmPath, language, "BattleEffectTexts");
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

    private static string _asmPath = Path.GetDirectoryName(typeof(UltraSandBagMOD).Assembly.Location);

    private static class Walkdir
    {
        public static List<string> GetFilesRecursive(string path)
        {
            List<string> paths = new List<string>();
            List<string> stepped = new List<string>();

            Walkdir.Walk(paths, stepped, dirPath: path);

            return paths;
        }

        private static void Walk(List<string> paths, List<string> stepped, string dirPath)
        {
            if (!Directory.Exists(dirPath) || File.Exists(dirPath))
            {
                return;
            }

            if (stepped.Contains(dirPath))
            {
                return;
            }
            else
            {
                stepped.Add(dirPath);
            }

            string[] files = Directory.GetFiles(dirPath);

            paths.AddRange(files);

            foreach (string dir in Directory.GetDirectories(dirPath))
            {
                Walkdir.Walk(paths, stepped, dir);
            }
        }
    }

    [HarmonyPatch(typeof(LocalizedTextLoader), "LoadBattleEffectTexts")]
    private static class LocalizePatch_EffectTexts
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            MethodInfo target = AccessTools.Method(typeof(BattleEffectTextsXmlList), "Init");
            MethodInfo inject = AccessTools.Method(typeof(ModResource), "LoadBattleEffectTextsXmls");

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
    }
}
