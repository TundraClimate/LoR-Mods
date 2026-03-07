using HarmonyLib;

namespace AdvancedBases
{
    public class AdvancedPassiveBase : PassiveAbilityBase
    {
        static AdvancedPassiveBase()
        {
            var harmony = new Harmony("AdvancedBases.Passive");

            harmony.CreateClassProcessor(typeof(PassivePatch.PatchOnBattleLast)).Patch();
            harmony.CreateClassProcessor(typeof(PassivePatch.PatchOnRoundStartFirst)).Patch();
            harmony.CreateClassProcessor(typeof(PassivePatch.PatchOnRoundStartLast)).Patch();
        }

        public virtual void OnRoundStartFirst()
        {
        }

        public virtual void OnRoundStartLast()
        {
        }

        public virtual bool IsAllowRoundEnd()
        {
            return true;
        }
    }

    static class PassivePatch
    {
        [HarmonyPatch(typeof(BattleUnitModel), "OnRoundStart_ignoreDead")]
        internal static class PatchOnRoundStartFirst
        {
            static void Prefix(BattleUnitModel __instance)
            {
                foreach (var passive in __instance.passiveDetail.PassiveList)
                {
                    if (passive is AdvancedPassiveBase)
                    {
                        ((AdvancedPassiveBase)passive).OnRoundStartFirst();
                    }
                }
            }
        }

        [HarmonyPatch(typeof(BattleUnitModel), "OnRoundStart_after")]
        internal static class PatchOnRoundStartLast
        {
            static void Postfix(BattleUnitModel __instance)
            {
                foreach (var passive in __instance.passiveDetail.PassiveList)
                {
                    if (passive is AdvancedPassiveBase)
                    {
                        ((AdvancedPassiveBase)passive).OnRoundStartLast();
                    }
                }
            }
        }

        [HarmonyPatch(typeof(StageController), "SetCurrentDiceActionPhase")]
        internal static class PatchOnBattleLast
        {
            static void Postfix(ref StageController.StagePhase ____phase)
            {
                if (____phase == StageController.StagePhase.RoundEndPhase)
                {
                    var all = BattleObjectManager.instance.GetAliveList(false);

                    foreach (var unit in all)
                    {
                        foreach (var passive in unit.passiveDetail.PassiveList)
                        {
                            if (passive is AdvancedPassiveBase && !((AdvancedPassiveBase)passive).IsAllowRoundEnd())
                            {
                                ____phase = StageController.StagePhase.SetCurrentDiceAction;
                            }
                        }
                    }
                }
            }
        }
    }
}
