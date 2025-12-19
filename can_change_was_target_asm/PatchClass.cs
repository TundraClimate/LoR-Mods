using HarmonyLib;

namespace CanChangeWasTargetMod
{
    public class PatchClass
    {
        [HarmonyPatch(typeof(BattleUnitModel), "CanChangeAttackTarget")]
        public class PostfixPatch_CanChangeAttackTarget
        {
            public static void Postfix(BattleUnitModel __instance, ref bool __result, BattleUnitModel target, int myIndex, int targetIndex)
            {
                BattlePlayingCardDataInUnitModel targetModel = target.view.speedDiceSetterUI.GetSpeedDiceByIndex(targetIndex).CardInDice;
                BattlePlayingCardDataInUnitModel selfModel = __instance.view.speedDiceSetterUI.GetSpeedDiceByIndex(myIndex).CardInDice;
                bool was_target = targetModel.earlyTargetOrder == selfModel.slotOrder && targetModel.earlyTarget == __instance;

                __result = __result || was_target;
            }
        }
    }
}
