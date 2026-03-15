using System.Reflection;
using HarmonyLib;
using DeviceOfHermes;
using DeviceOfHermes.AdvancedBase;

public class TestMOD : ModInitializer
{
    public static string packageId
    {
        get
        {
            return "TestMOD";
        }
    }

    public override void OnInitializeMod()
    {
        TestMOD.MutePatch();
        TestMOD.ApplyHarmonyPatch();
        DebugConsole.Open();

        ModResource.LoadAdditionals();

        new AdditonalOnlyCard(new LorId(260004)).AddCards(new LorId(705011));
    }

    private static void ApplyHarmonyPatch()
    {
        Harmony harmony = new Harmony(TestMOD.packageId);
        foreach (Type type in typeof(PatchClass).GetNestedTypes(AccessTools.all))
        {
            harmony.CreateClassProcessor(type).Patch();
        }
    }

    private static void MutePatch()
    {
        Harmony harmony = new Harmony(TestMOD.packageId + ".MutePatch");

        MethodInfo postfix = typeof(TestMOD).GetMethod("MuteSameAssembly", BindingFlags.Static | BindingFlags.NonPublic);

        harmony.Patch(typeof(Mod.ModContentManager).GetMethod("GetErrorLogs"), postfix: new HarmonyMethod(postfix));
    }

    private static void MuteSameAssembly(ref List<string> __result)
    {
        List<string> bin = new List<string>();

        foreach (string err in __result)
        {
            if (err.Contains("The same assembly name already exists."))
            {
                bin.Add(err);
            }
        }

        foreach (string trash in bin)
        {
            __result.Remove(trash);
        }
    }

    public class DiceCardAbility_TestAdvDice : AdvancedDiceBase
    {
        public override void OnAddToKeeped()
        {
            UnityEngine.Debug.Log("Added");
        }

        public override bool IsKeeps()
        {
            UnityEngine.Debug.Log("Keeps");

            return false;
        }
    }

    public class DiceCardSelfAbility_TestAdvCard : AdvancedCardBase
    {
        public override bool IsClashable => true;

        public override bool IsIgnoreSpeedByMatch => true;
    }

    public class PassiveAbility_TestAdvPassive : AdvancedPassiveBase
    {
        public override void OnRoundStartFirst()
        {
            UnityEngine.Debug.Log("RoundStartFirst");
        }

        public override void OnRoundStart()
        {
            UnityEngine.Debug.Log("RoundStart");

            var buf = base.owner.GetBufAndInitIfNull(() => new BattleUnitBuf_TestCustomBuf());
        }

        public override void OnRoundStartAfter()
        {
            UnityEngine.Debug.Log("RoundStartAfter");
        }

        public override void OnRoundStartLast()
        {
            UnityEngine.Debug.Log("RoundStartLast");
        }

        public override bool IsAllowRoundEnd()
        {
            UnityEngine.Debug.Log("Allow end");

            return base.IsAllowRoundEnd();
        }
    }
}
