using System;
using HarmonyLib;

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
}
