using System;
using System.Collections.Generic;
using HarmonyLib;

public class InBattleScheduler
{
    public static InBattleScheduler Instance
    {
        get
        {
            if (InBattleScheduler._instance == null)
            {
                InBattleScheduler._instance = new InBattleScheduler();
            }

            return InBattleScheduler._instance;
        }
    }

    private InBattleScheduler()
    {
        Harmony harmony = new Harmony(TestMOD.packageId + ".Schedule");

        harmony.CreateClassProcessor(typeof(SchedulePatch_OnRoundStart)).Patch();
        harmony.CreateClassProcessor(typeof(SchedulePatch_OnRollSpeedDice)).Patch();
        harmony.CreateClassProcessor(typeof(SchedulePatch_OnDrawCardAtStart)).Patch();
        harmony.CreateClassProcessor(typeof(SchedulePatch_OnApplyEnemyCards)).Patch();
        harmony.CreateClassProcessor(typeof(SchedulePatch_OnReachApplyCard)).Patch();
        harmony.CreateClassProcessor(typeof(SchedulePatch_OnStartBattle)).Patch();
        harmony.CreateClassProcessor(typeof(SchedulePatch_OnUseCard)).Patch();
        harmony.CreateClassProcessor(typeof(SchedulePatch_OnRoundEnd)).Patch();
    }

    public void AddSchedule(ScheduleTime time, Action schedule)
    {
        if (this._schedule.ContainsKey(time))
        {
            this._schedule[time] += schedule;
        }
        else
        {
            this._schedule.Add(time, schedule);
        }
    }

    private void InvokeSchedule(ScheduleTime time)
    {
        Action action;
        if (this._schedule.TryGetValue(time, out action))
        {
            action.Invoke();
        }
    }

    private static InBattleScheduler _instance;

    private Dictionary<ScheduleTime, Action> _schedule = new Dictionary<ScheduleTime, Action>();

    private bool _isReachedCardEquipPhase = false;

    public enum ScheduleTime
    {
        RoundStart,
        RoundStartAfter,
        RollSpeedDice,
        DrawCardAtRoundStart,
        ApplyEnemyCards,
        ReachCardEquip,
        StartBattle,
        UseCard,
        EndBattle,
        RoundEnd,
    }

    [HarmonyPatch(typeof(StageController), "RoundStartPhase_System")]
    private static class SchedulePatch_OnRoundStart
    {
        private static void Prefix(bool ____bCalledRoundStart_system)
        {
            if (!____bCalledRoundStart_system)
            {
                InBattleScheduler.Instance.InvokeSchedule(ScheduleTime.RoundStart);
                InBattleScheduler.Instance.InvokeSchedule(ScheduleTime.RoundStartAfter);
                InBattleScheduler.Instance._isReachedCardEquipPhase = false;
            }
        }
    }

    [HarmonyPatch(typeof(StageController), "SortUnitPhase")]
    private static class SchedulePatch_OnRollSpeedDice
    {
        private static void Prefix()
        {
            InBattleScheduler.Instance.InvokeSchedule(ScheduleTime.RollSpeedDice);
        }
    }

    [HarmonyPatch(typeof(StageController), "DrawCardPhase")]
    private static class SchedulePatch_OnDrawCardAtStart
    {
        private static void Prefix()
        {
            InBattleScheduler.Instance.InvokeSchedule(ScheduleTime.DrawCardAtRoundStart);
        }
    }

    [HarmonyPatch(typeof(StageController), "ApplyEnemyCardPhase")]
    private static class SchedulePatch_OnApplyEnemyCards
    {
        private static void Prefix()
        {
            InBattleScheduler.Instance.InvokeSchedule(ScheduleTime.ApplyEnemyCards);
        }
    }

    [HarmonyPatch(typeof(StageController), "ApplyLibrarianCardPhase")]
    private static class SchedulePatch_OnReachApplyCard
    {
        private static void Prefix()
        {
            if (!InBattleScheduler.Instance._isReachedCardEquipPhase)
            {
                InBattleScheduler.Instance.InvokeSchedule(ScheduleTime.ReachCardEquip);
                InBattleScheduler.Instance._isReachedCardEquipPhase = true;
            }
        }
    }

    [HarmonyPatch(typeof(StageController), "ArrangeCardsPhase")]
    private static class SchedulePatch_OnStartBattle
    {
        private static void Prefix()
        {
            InBattleScheduler.Instance.InvokeSchedule(ScheduleTime.StartBattle);
        }
    }

    [HarmonyPatch(typeof(StageController), "SetCurrentDiceActionPhase")]
    private static class SchedulePatch_OnUseCard
    {
        private static void Prefix(List<BattlePlayingCardDataInUnitModel> ____allCardList)
        {
            if (____allCardList.Count > 0)
            {
                InBattleScheduler.Instance.InvokeSchedule(ScheduleTime.UseCard);
            }
        }

        private static void Postfix(StageController.StagePhase ____phase)
        {
            if (____phase == StageController.StagePhase.RoundEndPhase)
            {
                InBattleScheduler.Instance.InvokeSchedule(ScheduleTime.EndBattle);
            }
        }
    }

    [HarmonyPatch(typeof(StageController), "RoundEndPhase_ExpandMap")]
    private static class SchedulePatch_OnRoundEnd
    {
        private static void Prefix()
        {
            InBattleScheduler.Instance.InvokeSchedule(ScheduleTime.RoundEnd);
        }
    }
}
