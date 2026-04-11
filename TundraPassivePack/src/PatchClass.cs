using HarmonyLib;
using UI;

public static class PatchClass
{
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
            var id = new LorId(TundraPassivePack.packageId, 10000001);

            for (int i = BookInventoryModel.Instance.GetBookCount(id); 5 > i; i++)
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
