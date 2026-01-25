public class BattleUnitBuf_TheTerminateAll : PrescriptBuf
{
    protected override string keywordId
    {
        get
        {
            return "TheTerminateAll";
        }
    }

    public override bool IsIndexMarkNeeds(BattleDiceCardModel model)
    {
        LorId[] pids = new[] { new LorId(PowerfulEstherMOD.packageId, 40) };

        return pids.Contains(model.GetID());
    }
}
