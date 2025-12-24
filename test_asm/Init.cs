using System;
using HarmonyLib;
using System.Collections.Generic;
using LOR_DiceSystem;

namespace TestMod
{
    public class Test : ModInitializer
    {
        public static string PackageId
        {
            get
            {
                return "TestMod";
            }
        }

        public override void OnInitializeMod()
        {
            Harmony harmony = new Harmony(Test.PackageId);
            foreach (Type type in typeof(PatchClass).GetNestedTypes(AccessTools.all))
            {
                harmony.CreateClassProcessor(type).Patch();
            }

            ItemXmlDataList.instance.AddCardInfoByMod("Ah, id not here...", new List<DiceCardXmlInfo>()
            {
                new DiceCardXmlInfo(new LorId(Test.PackageId, 1))
                {
                    workshopName = "めるちぇ",
                    /* Artwork = CardResourceManager.Instance.GetArtworkSpriteByCardID(612001).name, */
                    Rarity = Rarity.Uncommon,
                    optionList = new List<CardOption>() { CardOption.ExhaustOnUse, CardOption.NoInventory },
                    Spec = new DiceCardSpec()
                    {
                        Cost = 0,
                        Ranged = CardRange.Near,
                    },
                    DiceBehaviourList = new List<DiceBehaviour>()
                    {
                        new DiceBehaviour()
                        {
                            Min = 4,
                            Dice = 8,
                            Type = BehaviourType.Atk,
                            Detail = BehaviourDetail.Penetrate,
                            MotionDetail = MotionDetail.Z,
                        },
                    },
                    Chapter = 5,
                    Priority = 0,
                }
            });
        }
    }
}
