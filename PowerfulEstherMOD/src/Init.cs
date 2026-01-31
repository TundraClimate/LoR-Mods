using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;

public class PowerfulEstherMOD : ModInitializer
{
    public static string packageId
    {
        get
        {
            return "PowerfulEstherMOD";
        }
    }

    public override void OnInitializeMod()
    {
        PowerfulEstherMOD.ApplyHarmonyPatch();
        PowerfulEstherMOD.DebugInitialize();
        ModResource.LoadAdditionals();
    }

    private static void ApplyHarmonyPatch()
    {
        Harmony harmony = new Harmony(PowerfulEstherMOD.packageId);
        foreach (Type type in typeof(PatchClass).GetNestedTypes(AccessTools.all))
        {
            harmony.CreateClassProcessor(type).Patch();
        }
    }

    private static void DebugInitialize()
    {
        DebugUtil.InBattleScheduler.Instance.AddSchedule(DebugUtil.InBattleScheduler.ScheduleTime.RoundStartAfter, () =>
        {
            BattleUnitModel esther = BattleObjectManager.instance.GetAliveList(Faction.Player).Find(pl => pl.passiveDetail.HasPassive<PassiveAbility_Prescript>());

            if (esther == null)
            {
                return;
            }

            esther.allyCardDetail.ExhaustAllCards();
            esther.allyCardDetail.AddNewCard(new LorId(packageId, 23));
            esther.allyCardDetail.AddNewCard(new LorId(packageId, 23));
            esther.allyCardDetail.AddNewCard(new LorId(packageId, 23));
            esther.allyCardDetail.AddNewCard(new LorId(packageId, 23));
        });
    }

    static PowerfulEstherMOD()
    {
        AppDomain.CurrentDomain.AssemblyResolve += ResolveAsm;
    }

    static Assembly ResolveAsm(object _, ResolveEventArgs e)
    {
        Assembly asm = Assembly.GetExecutingAssembly();
        string res = new List<string>(asm.GetManifestResourceNames())
            .Find(n => n.EndsWith("DebugUtil.dll", StringComparison.OrdinalIgnoreCase));

        if (res == null)
        {
            return null;
        }

        using (var s = asm.GetManifestResourceStream(res))
        {
            byte[] buf = new byte[s.Length];
            s.Read(buf, 0, buf.Length);

            return Assembly.Load(buf);
        }
    }
}
