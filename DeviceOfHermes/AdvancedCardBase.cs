using HarmonyLib;

namespace DeviceOfHermes.AdvancedBase;

public class AdvancedCardBase : DiceCardSelfAbilityBase
{
    static AdvancedCardBase()
    {
        var harmony = new Harmony("DeviceOfHermes.AdvancedBases.Card");

        harmony.CreateClassProcessor(typeof(CardPatch.PatchTargetUI)).Patch();
        harmony.CreateClassProcessor(typeof(CardPatch.PatchOnStartResolve)).Patch();
        harmony.CreateClassProcessor(typeof(CardPatch.PatchOnDynamicParrying)).Patch();
    }

    public virtual bool IsClashable => true;
}

internal class CardPatch
{
    [HarmonyPatch(typeof(BattleUnitTargetArrowManagerUI), "UpdateTargetListData")]
    internal static class PatchTargetUI
    {
        static void Postfix(List<BattleUnitTargetArrowData> ___TargetListData)
        {
            foreach (var arrow in ___TargetListData)
            {
                var forceUnpair = false;

                var selfAbi = arrow?.Dice?.CardInDice?.cardAbility;

                if (selfAbi is AdvancedCardBase selfAdvAbi && !selfAdvAbi.IsClashable)
                {
                    forceUnpair = true;
                }

                var targetAbi = arrow?.TargetDice?.CardInDice?.cardAbility;

                if (targetAbi is AdvancedCardBase targetAdvAbi && !targetAdvAbi.IsClashable)
                {
                    forceUnpair = true;
                }

                if (forceUnpair)
                {
                    arrow?.isPairing = false;
                }
            }
        }
    }

    [HarmonyPatch(typeof(StageController), "ArrangeCardsPhase")]
    internal class PatchOnStartResolve
    {
        static void Postfix(ref List<BattlePlayingCardDataInUnitModel> ____allCardList)
        {
            foreach (var card in ____allCardList)
            {
                if (card?.cardAbility is AdvancedCardBase selfAdvAbi && !selfAdvAbi.IsClashable)
                {
                    card?.targetSlotOrder = -1;
                }
            }
        }
    }

    [HarmonyPatch(typeof(StageController), "StartParrying")]
    internal class PatchOnDynamicParrying
    {
        static Action<StageController, BattlePlayingCardDataInUnitModel> startAction;

        static PatchOnDynamicParrying()
        {
            var act = AccessTools.Method(typeof(StageController), "StartAction");

            if (act is null)
            {
                throw new InvalidOperationException("The StageController::StartAction cannot access.");
            }

            startAction = (Action<StageController, BattlePlayingCardDataInUnitModel>)act.CreateDelegate(typeof(Action<StageController, BattlePlayingCardDataInUnitModel>));
        }

        static bool Prefix(StageController __instance, BattlePlayingCardDataInUnitModel cardA)
        {
            if (cardA?.cardAbility is AdvancedCardBase advAbi && !advAbi.IsClashable)
            {
                startAction(__instance, cardA);

                return false;
            }

            return true;
        }
    }
}
