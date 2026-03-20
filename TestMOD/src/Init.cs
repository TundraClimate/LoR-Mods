using System.Reflection;
using HarmonyLib;
using DeviceOfHermes;
using DeviceOfHermes.AdvancedBase;
using DeviceOfHermes.CustomDice;

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

    public class DiceCardAbility_Revenge : RevengeDice
    {
        public static string Desc = "復讐ダイス".Purple();

        public override void OnRevenge(BattlePlayingCardDataInUnitModel card, BattleDiceBehavior revengeBy)
        {
        }
    }

    public class DiceCardSelfAbility_TestAdvCard : AdvancedCardBase
    {
        public override bool IsClashable => true;

        public override bool IsIgnoreSpeedByMatch => true;

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            base.owner.ConsumeAmmo<TestAmmoBuf>(6);
        }
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
            var ammo = base.owner.GetBufAndInitIfNull(() => new TestAmmoBuf());
            var reload = base.owner.GetBufAndInitIfNull(() => new ReloadAmmoBuf<TestAmmoBuf>());
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

    public class TestAmmoBuf : BattleAmmoBuf
    {
        protected override string keywordId => "TestCustomBuf";

        public override int DefaultStack => 6;

        public override bool DiceBlockWithNotConsumable => true;

        public override void OnConsume(ref int num)
        {
            Hermes.Say($"Wowow consumed {num} stack");
        }
    }
}
