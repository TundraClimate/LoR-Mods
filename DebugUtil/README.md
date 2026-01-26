# depends

- HarmonyLib net48
- LOR Assembly-CSharp

# H2U

## Scheduler

```cs
using DebugUtil;

// Logging the "Round Started!" for every OnRoundStart calling
InBattleScheduler.Instance.AddSchedule(InBattleScheduler.ScheduleTime.RoundStart, () =>
{
    UnityEngine.Debug.Log("Round Started!");
});

```
