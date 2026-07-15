using HarmonyLib;

public static class PatchClass
{
    [HarmonyPatch(typeof(BookInventoryModel), "LoadFromSaveData")]
    class PatchOn
    {
        static void Postfix()
        {
            AddBook(new LorId(DeathThumb.packageId, 10000001), 5);
        }

        static void AddBook(LorId id, int needs)
        {
            for (int i = BookInventoryModel.Instance.GetBookCount(id); needs > i; i++)
            {
                BookInventoryModel.Instance.CreateBook(id);
            }
        }
    }
}
