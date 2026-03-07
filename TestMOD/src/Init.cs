using System;
using System.Reflection;
using System.Collections.Generic;
using HarmonyLib;
using AdvancedBases;

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

    public class PassiveAbility_TestAdvPassive : AdvancedPassiveBase
    {
        public override void OnRoundStartFirst()
        {
            UnityEngine.Debug.Log("RoundStartFirst");
        }

        public override void OnRoundStart()
        {
            UnityEngine.Debug.Log("RoundStart");
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
