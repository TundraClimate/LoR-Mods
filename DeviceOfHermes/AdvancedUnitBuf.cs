using HarmonyLib;

namespace DeviceOfHermes.AdvancedBase;

public class AdvancedUnitBuf : BattleUnitBuf
{
    static AdvancedUnitBuf()
    {
        var harmony = new Harmony("DeviceOfHermes.AdvancedBases.UnitBuf");

        harmony.CreateClassProcessor(typeof(UnitBufPatch.PatchObserver)).Patch();
    }

    public override void Init(BattleUnitModel owner)
    {
        base.Init(owner);

        this.lastStack = this.DefaultStack;
        this.stack = this.DefaultStack;
    }

    public virtual int DefaultStack { get => 0; }

    public virtual void OnStackChange(int last)
    {
    }

    internal int lastStack;
}

internal class UnitBufPatch
{
    [HarmonyPatch(typeof(StageController), "OnFixedUpdate")]
    internal static class PatchObserver
    {
        static void Postfix()
        {
            var alives = BattleObjectManager.instance.GetAliveList();

            foreach (var unit in alives)
            {
                foreach (var buf in unit.bufListDetail?.GetActivatedBufList() ?? new())
                {
                    if (buf is AdvancedUnitBuf advBuf && advBuf.stack != advBuf.lastStack)
                    {
                        advBuf.OnStackChange(advBuf.lastStack);

                        advBuf.lastStack = advBuf.stack;
                    }
                }
            }
        }
    }
}
