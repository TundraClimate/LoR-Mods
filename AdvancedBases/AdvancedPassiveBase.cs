using HarmonyLib;

namespace AdvancedBases
{
    public class AdvancedPassiveBase : PassiveAbilityBase
    {
        static AdvancedPassiveBase()
        {
            var harmony = new Harmony("AdvancedBases");

            harmony.CreateClassProcessor(typeof(PassivePatch.PatchOnBattleLast)).Patch();
        }

        public virtual bool IsAllowRoundEnd()
        {
            return true;
        }
    }

    static class PassivePatch
    {
        [HarmonyPatch(typeof(StageController), "SetCurrentDiceActionPhase")]
        public static class PatchOnBattleLast
        {
            static void Postfix(ref StageController.StagePhase ____phase)
            {
                if (____phase == StageController.StagePhase.RoundEndPhase)
                {
                    var all = BattleObjectManager.instance.GetList();

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
