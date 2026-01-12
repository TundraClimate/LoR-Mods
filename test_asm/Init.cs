using System;
using System.IO;
using HarmonyLib;
using UnityEngine;

namespace TestMod
{
    public class Test : ModInitializer
    {
        public static string packageId
        {
            get
            {
                return "TestMod";
            }
        }

        public override void OnInitializeMod()
        {
            Harmony harmony = new Harmony(Test.packageId);
            foreach (Type type in typeof(PatchClass).GetNestedTypes(AccessTools.all))
            {
                harmony.CreateClassProcessor(type).Patch();
            }

            RegisterResources();
            LocalizedTextLoader.Instance.LoadBattleEffectTexts(GlobalGameManager.Instance.CurrentOption.language);
        }

        private void RegisterResources()
        {
            string resourcesPath = string.Format("{0}\\Mods\\Test\\Resource\\", Application.dataPath);

            BattleUnitBuf._bufIconDictionary.Add(BattleUnitBuf_TestCustomBuf.id, GenSprite(resourcesPath + "TestCustomBuf.png"));
        }

        private Sprite GenSprite(string path)
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

        /* private void AddCards()
        {
            CardFactory fa = new CardFactory();
            List<DiceCardXmlInfo> cardList = new List<DiceCardXmlInfo>();

            fa.SetName("パンチ");
            fa.SetCardCost(3);
            fa.SetArtwork("PunchCard");
            fa.SetRarity(Rarity.Unique);

            cardList.Add(fa.Generate(new LorId(packageId, 1)));

            fa.SetName("キック");
            fa.SetCardCost(2);
            fa.SetArtwork("KickCard");
            fa.SetRarity(Rarity.Uncommon);

            cardList.Add(fa.Generate(new LorId(packageId, 2)));

            ItemXmlDataList.instance.AddCardInfoByMod("Ah, id not here...", cardList);
        } */
    }

    public class BattleUnitBuf_TestCustomBuf : BattleUnitBuf
    {
        public static string id
        {
            get
            {
                return string.Format("{0}_TestCustomBuf", Test.packageId);
            }
        }

        protected override string keywordId
        {
            get
            {
                return BattleUnitBuf_TestCustomBuf.id;
            }
        }

        public override void OnRoundEnd()
        {
            base.OnRoundEnd();
            base.Destroy();
        }
    }
}
