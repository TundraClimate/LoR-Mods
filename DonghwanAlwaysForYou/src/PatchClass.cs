using HarmonyLib;
using DeviceOfHermes;

public static class PatchClass
{
    [HarmonyPatch(typeof(StageController), "RoundStartPhase_System")]
    class OnRegister
    {
        static void Prefix(StageController __instance, bool ____bCalledRoundStart_system)
        {
            if (!____bCalledRoundStart_system)
            {
                try
                {
                    foreach (var unit in BattleObjectManager.instance.GetList())
                    {
                        if (unit.UnitData.unitData.bookItem.BookId == 143005)
                        {
                            continue;
                        }

                        if (unit.faction is Faction.Player)
                        {
                            if (StageController.Instance.TryAddNewLibrarian(new LorId(43005), unit.index, unit.UnitData.unitData.customizeData.height))
                            {
                            }
                        }
                        else
                        {
                            if (StageController.Instance.TryAddNewEnemy(new LorId(43005), unit.index, unit.UnitData.unitData.customizeData.height))
                            {
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }

                BattleObjectManager.instance.InitUI();
            }
        }
    }
}
