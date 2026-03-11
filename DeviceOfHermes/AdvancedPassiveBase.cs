using HarmonyLib;

namespace DeviceOfHermes.AdvancedBase;

public class AdvancedPassiveBase : PassiveAbilityBase
{
    static AdvancedPassiveBase()
    {
        var harmony = new Harmony("DeviceOfHermes.AdvancedBases.Passive");

        harmony.CreateClassProcessor(typeof(PassivePatch.PatchOnBattleLast)).Patch();
        harmony.CreateClassProcessor(typeof(PassivePatch.PatchOnRoundStartFirst)).Patch();
        harmony.CreateClassProcessor(typeof(PassivePatch.PatchOnRoundStartLast)).Patch();
        harmony.CreateClassProcessor(typeof(PassivePatch.PatchTargetUI)).Patch();
        harmony.CreateClassProcessor(typeof(PassivePatch.PatchOnStartResolve)).Patch();
        harmony.CreateClassProcessor(typeof(PassivePatch.PatchOnDynamicParrying)).Patch();
    }

    public virtual void OnRoundStartFirst()
    {
    }

    public virtual void OnRoundStartLast()
    {
    }

    public virtual bool IsClashable(BattlePlayingCardDataInUnitModel card)
    {
        return true;
    }

    public virtual bool IsClashable(BattlePlayingCardDataInUnitModel self, BattlePlayingCardDataInUnitModel target)
    {
        return true;
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

    [HarmonyPatch(typeof(BattleUnitTargetArrowManagerUI), "UpdateTargetListData")]
    internal static class PatchTargetUI
    {
        static void Postfix(List<BattleUnitTargetArrowData> ___TargetListData)
        {
            foreach (var arrow in ___TargetListData)
            {
                var forceUnpair = false;

                var selfCard = arrow?.Dice?.CardInDice;
                var targetCard = arrow?.TargetDice?.CardInDice;

                if (selfCard is null || targetCard is null)
                {
                    continue;
                }

                foreach (var passive in selfCard.owner?.passiveDetail?.PassiveList ?? new())
                {
                    if (passive is AdvancedPassiveBase advPassive)
                    {
                        if (!advPassive.IsClashable(selfCard) || !advPassive.IsClashable(selfCard, targetCard))
                        {
                            forceUnpair = true;
                        }
                    }
                }

                foreach (var passive in targetCard.owner?.passiveDetail?.PassiveList ?? new())
                {
                    if (passive is AdvancedPassiveBase advPassive)
                    {
                        if (!advPassive.IsClashable(targetCard) || !advPassive.IsClashable(targetCard, selfCard))
                        {
                            forceUnpair = true;
                        }
                    }
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
                var targetAry = card?.target?.cardSlotDetail?.cardAry;
                var targetSlotOrder = card?.targetSlotOrder ?? -1;

                if (targetAry is null || targetSlotOrder < 0)
                {
                    continue;
                }

                var target = targetAry[targetSlotOrder];

                var isClashableSelf = card?.owner?.passiveDetail?.PassiveList?.All(passive => !(passive is AdvancedPassiveBase adv && (!adv.IsClashable(card) || !adv.IsClashable(card, target)))) ?? true;

                var isClashableTarget = target.owner?.passiveDetail?.PassiveList?.All(passive => !(passive is AdvancedPassiveBase adv && (!adv.IsClashable(target) || (card is not null && !adv.IsClashable(target, card))))) ?? true;

                if (!isClashableSelf || !isClashableTarget)
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

        static bool Prefix(StageController __instance, BattlePlayingCardDataInUnitModel cardA, BattlePlayingCardDataInUnitModel cardB)
        {
            var isClashableA = cardA.owner?.passiveDetail?.PassiveList?.All(passive => !(passive is AdvancedPassiveBase adv && (!adv.IsClashable(cardA) || !adv.IsClashable(cardA, cardB)))) ?? true;

            var isClashableB = cardB.owner?.passiveDetail?.PassiveList?.All(passive => !(passive is AdvancedPassiveBase adv && (!adv.IsClashable(cardB) || !adv.IsClashable(cardB, cardA)))) ?? true;

            if (!isClashableA || !isClashableB)
            {
                startAction(__instance, cardA);

                return false;
            }

            return true;
        }
    }
}
