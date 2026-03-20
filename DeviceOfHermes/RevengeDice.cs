using System.Reflection.Emit;
using LOR_DiceSystem;
using HarmonyLib;
using UnityEngine;
using DeviceOfHermes.AdvancedBase;

namespace DeviceOfHermes.CustomDice;

public class RevengeDice : AdvancedDiceBase
{
    static RevengeDice()
    {
        CustomDiceSprite.AddSequence(
            beh =>
                beh.Type is BehaviourType.Standby &&
                beh.Detail == BehaviourDetail.Slash &&
                AssemblyManager.Instance.CreateInstance_DiceCardAbility(beh.Script) is RevengeDice,
            HermesConstants.RevengeDiceSlash,
            new Color(255, 0, 200, 200)
        );
        CustomDiceSprite.AddSequence(
            beh =>
                beh.Type is BehaviourType.Standby &&
                beh.Detail == BehaviourDetail.Penetrate &&
                AssemblyManager.Instance.CreateInstance_DiceCardAbility(beh.Script) is RevengeDice,
            HermesConstants.RevengeDicePenetrate,
            new Color(255, 0, 200, 200)
        );
        CustomDiceSprite.AddSequence(
            beh =>
                beh.Type is BehaviourType.Standby &&
                beh.Detail == BehaviourDetail.Hit &&
                AssemblyManager.Instance.CreateInstance_DiceCardAbility(beh.Script) is RevengeDice,
            HermesConstants.RevengeDiceHit,
            new Color(255, 0, 200, 200)
        );

        var harmony = new Harmony("DeviceOfHermes.CustomDice");

        harmony.CreateClassProcessor(typeof(PatchRevengeDice.PatchBehaviourToKeeps)).Patch();
    }
}

internal class PatchRevengeDice
{
    [HarmonyPatch(typeof(BattleKeepedCardDataInUnitModel), "AddBehaviours", [typeof(BattleDiceCardModel), typeof(List<BattleDiceBehavior>)])]
    public class PatchBehaviourToKeeps
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var target = AccessTools.Method(typeof(Queue<BattleDiceBehavior>), "Enqueue");
            var inject = AccessTools.Method(typeof(PatchBehaviourToKeeps), "InjectMethod");

            var matcher = new CodeMatcher(instructions);

            matcher.MatchStartForward(CodeMatch.Calls(target))
                .SetInstruction(new CodeInstruction(OpCodes.Call, inject));

            return matcher.Instructions();
        }

        static void InjectMethod(Queue<BattleDiceBehavior> cardBehaviorQueue, BattleDiceBehavior beh)
        {
            if (beh.abilityList.Exists(abi => abi is RevengeDice))
            {
            }
            else
            {
                cardBehaviorQueue.Enqueue(beh);
            }
        }
    }
}
