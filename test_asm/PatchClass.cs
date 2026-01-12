using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using LOR_XML;

namespace TestMod
{
    public class PatchClass
    {
        static bool flag = false;

        [HarmonyPatch(typeof(StageController), "RoundStartPhase_System")]
        public class Patch_Prefix_RoundStartPhase_System
        {
            public static void Prefix()
            {
                if (!PatchClass.flag)
                {
                    foreach (BattleUnitModel battleUnitModel in BattleObjectManager.instance.GetAliveList(false))
                    {
                        battleUnitModel.bufListDetail.AddBuf(new TestMod.BattleUnitBuf_TestCustomBuf());
                    }

                    PatchClass.flag = true;
                }
            }
        }

        [HarmonyPatch(typeof(LocalizedTextLoader), "LoadBattleEffectTexts")]
        public class Transpile_BattleEffectTexts_Localize
        {
            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                MethodInfo target = AccessTools.Method(typeof(BattleEffectTextsXmlList), "Init");

                foreach (CodeInstruction inst in instructions)
                {
                    if (inst.Calls(target))
                    {
                        yield return new CodeInstruction(OpCodes.Dup);
                        yield return new CodeInstruction(OpCodes.Ldarg_1);
                        yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(PatchClass.Transpile_BattleEffectTexts_Localize), "InfixPatch"));
                    }

                    yield return inst;
                }
            }

            public static void InfixPatch(Dictionary<string, BattleEffectText> dictionary, string language)
            {
                string lang = language.ToLower();

                if (lang == "jp")
                {
                    AddDict(dictionary, BattleUnitBuf_TestCustomBuf.id, "すっごいテストなバフ", "ここDesc");
                }
                else
                {
                    // en, cn, trcn, kr
                    UnityEngine.Debug.LogError(string.Format("A language of {0} isn't supported.", language));
                }
            }

            private static void AddDict(Dictionary<string, BattleEffectText> dictionary, string id, string name, string desc)
            {
                BattleEffectText text = new BattleEffectText()
                {
                    ID = id,
                    Name = name,
                    Desc = desc,
                };

                dictionary.Add(id, text);
            }
        }
    }

    /* [HarmonyPatch(typeof(StageController), "SetCurrentDiceActionPhase")]
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
    } */
}
