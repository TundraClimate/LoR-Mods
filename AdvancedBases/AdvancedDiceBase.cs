using System.Collections.Generic;
using HarmonyLib;
using LOR_DiceSystem;

namespace AdvancedBases
{
    public class AdvancedDiceBase : DiceCardAbilityBase
    {
        static AdvancedDiceBase()
        {
            var harmony = new Harmony("AdvancedBases.Dice");

            harmony.CreateClassProcessor(typeof(DicePatch.PatchOnAddKeeps1)).Patch();
            harmony.CreateClassProcessor(typeof(DicePatch.PatchOnAddKeeps2)).Patch();
            harmony.CreateClassProcessor(typeof(DicePatch.PatchOnAddKeep)).Patch();
            harmony.CreateClassProcessor(typeof(DicePatch.PatchOnAddKeepForDef)).Patch();
        }

        public virtual void OnAddToKeeped()
        {
        }

        public virtual bool IsKeeps()
        {
            return true;
        }
    }

    static class DicePatch
    {
        static bool OnAddKeeped(List<BattleDiceBehavior> behaviourList)
        {
            var isBreak = false;

            foreach (var beh in behaviourList)
            {
                foreach (var abi in beh.abilityList)
                {
                    if (abi is AdvancedDiceBase)
                    {
                        var advAbi = (AdvancedDiceBase)abi;

                        advAbi.OnAddToKeeped();

                        if (!isBreak)
                        {
                            isBreak = !advAbi.IsKeeps();
                        }
                    }
                }
            }

            return !isBreak;
        }

        [HarmonyPatch
        (
            typeof(BattleKeepedCardDataInUnitModel),
            "AddBehaviours",
            new[] { typeof(DiceCardXmlInfo), typeof(List<BattleDiceBehavior>) }
        )]
        internal class PatchOnAddKeeps1
        {
            static bool Prefix(List<BattleDiceBehavior> behaviourList)
            {
                return DicePatch.OnAddKeeped(behaviourList);
            }
        }

        [HarmonyPatch
        (
            typeof(BattleKeepedCardDataInUnitModel),
            "AddBehaviours",
            new[] { typeof(BattleDiceCardModel), typeof(List<BattleDiceBehavior>) }
        )]
        internal class PatchOnAddKeeps2
        {
            static bool Prefix(List<BattleDiceBehavior> behaviourList)
            {
                return DicePatch.OnAddKeeped(behaviourList);
            }
        }

        [HarmonyPatch(typeof(BattleKeepedCardDataInUnitModel), "AddBehaviour")]
        internal class PatchOnAddKeep
        {
            static bool Prefix(BattleDiceBehavior behaviour)
            {
                return DicePatch.OnAddKeeped(new() { behaviour });
            }
        }

        [HarmonyPatch(typeof(BattleKeepedCardDataInUnitModel), "AddBehaviourForOnlyDefense")]
        internal class PatchOnAddKeepForDef
        {
            static bool Prefix(BattleDiceBehavior behaviour)
            {
                return DicePatch.OnAddKeeped(new() { behaviour });
            }
        }
    }
}
