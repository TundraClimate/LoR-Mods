using HarmonyLib;
using System.Collections.Generic;

namespace TestMod
{
    public class PatchClass
    {
        [HarmonyPatch(typeof(StageController), "SetCurrentDiceActionPhase")]
        public class Patch_Postfix_SetCurrentDiceActionPhase
        {
            public static void Postfix(ref List<BattlePlayingCardDataInUnitModel> ____allCardList, ref StageController.StagePhase ____phase)
            {
                if (____phase != StageController.StagePhase.RoundEndPhase) return;

                List<BattleUnitModel> alives = BattleObjectManager.instance.GetAliveList(Faction.Enemy);

                List<BattleUnitModel> stagger = alives.FindAll((BattleUnitModel model) => model.breakDetail.IsBreakLifeZero());

                if (stagger.Count == 0) return;

                List<BattleUnitModel> pl_alives = BattleObjectManager.instance.GetAliveList(Faction.Player);

                foreach (BattleUnitModel model in stagger)
                {
                    foreach (BattleUnitModel pl in pl_alives)
                    {
                        BattleDiceCardModel card =
                                BattleDiceCardModel.CreatePlayingCard(
                                    ItemXmlDataList.instance.GetCardItem(new LorId(Test.packageId, 1))
                                );

                        BattlePlayingCardDataInUnitModel playcard = new BattlePlayingCardDataInUnitModel()
                        {
                            owner = pl,
                            card = card,
                            target = model,
                            targetSlotOrder = 0,
                            earlyTarget = model,
                            earlyTargetOrder = 0,
                            cardAbility = card.CreateDiceCardSelfAbilityScript(),
                            speedDiceResultValue = 1,
                            slotOrder = 0,
                        };

                        card.CreateDiceCardBehaviorList().ForEach((BattleDiceBehavior beh) =>
                        {
                            beh.card = playcard;
                            beh.card.card = card;
                            playcard.cardBehaviorQueue.Enqueue(beh);
                        });

                        pl.currentDiceAction = playcard;

                        ____allCardList.Add(playcard);
                    }
                }

                ____phase = StageController.StagePhase.SetCurrentDiceAction;
            }
        }
    }
}
