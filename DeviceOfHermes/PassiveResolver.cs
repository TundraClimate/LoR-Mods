using HarmonyLib;

namespace DeviceOfHermes;

public static class PassiveResolver
{
    static PassiveResolver()
    {
        var harmony = new Harmony("DeviceOfHermes.PassiveResolver");

        harmony.CreateClassProcessor(typeof(PatchPassiveResolveState)).Patch();
    }

    public static void AddRestrict(Func<List<PassiveXmlInfo>, PassiveXmlInfo, bool> restrict)
    {
        _restricts.Add(restrict);
    }

    private static List<Func<List<PassiveXmlInfo>, PassiveXmlInfo, bool>> _restricts = new();

    [HarmonyPatch(typeof(BookModel), "CanSuccessionPassive")]
    class PatchPassiveResolveState
    {
        static void Postfix(BookModel __instance, PassiveModel targetpassive, ref bool __result, ref GivePassiveState haspassiveState)
        {
            if (targetpassive is null)
            {
                return;
            }

            var models = __instance.GetPassiveModelList();
            var originals = models.Map(model => model.reservedData.currentpassive).Filter(passive => passive is not null).ToList() ?? new();

            foreach (var restrict in _restricts)
            {
                if (restrict(originals, targetpassive.originpassive))
                {
                    haspassiveState = GivePassiveState.Lock;
                    __result = false;
                }
            }
        }
    }
}
