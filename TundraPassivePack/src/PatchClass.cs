using System.Reflection;
using HarmonyLib;
using UI;

public static class PatchClass
{
    [HarmonyPatch]
    class PatchMaxCost
    {
        static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(typeof(BattleUnitCostUI), "SetCurrentMaxCost");
            yield return AccessTools.Method(typeof(BattleUnitCostUI), "SetCurrentCost");
        }

        static Exception? Finalizer(Exception __exception)
        {
            if (__exception is ArgumentOutOfRangeException)
            {
                return null;
            }

            return __exception;
        }
    }

    [HarmonyPatch(typeof(BookModel), "get_ClassInfo")]
    class PatchSuccession
    {
        static void Postfix(ref BookXmlInfo __result)
        {
            if (__result.SuccessionPossibleNumber < 18)
            {
                __result.SuccessionPossibleNumber = 18;
            }
        }
    }

    [HarmonyPatch(typeof(BookModel), "IsNotFullEquipPassiveBook")]
    class PatchEquip
    {
        static void Postfix(BookModel __instance, ref bool __result)
        {
            if (__instance.reservedData.equipedBookIdListInPassive.Count < 8)
            {
                __result = true;
            }
        }
    }

    [HarmonyPatch(typeof(BookInventoryModel), "LoadFromSaveData")]
    class PatchOn
    {
        static void Postfix()
        {
            AddBook(new LorId(TundraPassivePack.packageId, 10000001), 5);
            AddBook(new LorId(TundraPassivePack.packageId, 10000002), 5);
            AddBook(new LorId(TundraPassivePack.packageId, 10000003), 5);
        }

        static void AddBook(LorId id, int needs)
        {
            for (int i = BookInventoryModel.Instance.GetBookCount(id); needs > i; i++)
            {
                BookInventoryModel.Instance.CreateBook(id);
            }
        }
    }

    [HarmonyPatch(typeof(UIPassiveSuccessionCenterPanel), "SetBooksData")]
    class PatchCenterUI
    {
        static void Prefix(List<UIPassiveSuccessionCenterEquipBookSlot> ___equipBookSlotList)
        {
            if (___equipBookSlotList.Count == 0)
            {
                return;
            }

            for (int i = ___equipBookSlotList.Count; 8 > i; i++)
            {
                var slot = UnityEngine.Object.Instantiate(___equipBookSlotList[0].gameObject, ___equipBookSlotList[0].transform.parent).GetComponent<UIPassiveSuccessionCenterEquipBookSlot>();

                ___equipBookSlotList.Add(slot);
            }

            foreach (var slot in ___equipBookSlotList)
            {
                slot.gameObject.SetActive(true);
            }
        }
    }
}
